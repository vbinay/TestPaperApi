using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPaperApi.Helper;
using TestPaperApi.Models;
using TestPaperApi.Services;

namespace TestPaperApi.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public readonly DatabaseContext _dbContext;
        public readonly PaymentSetting _paymentsetting;

        public PaymentController(DatabaseContext databaseContext, IOptions<PaymentSetting> options)
        {
            this._dbContext = databaseContext;
            _paymentsetting = options.Value;
        }

        [HttpPost("create-order")]
        public IActionResult CreateOrder([FromBody] PaymentRequest paymentRequest)
        {
            RazorpayClient client = new RazorpayClient(_paymentsetting.PaymentKey, _paymentsetting.PaymentSecret);

            Dictionary<string, object> options = new Dictionary<string, object>
            {
                { "amount", paymentRequest.Amount },  // Amount in paise
                { "currency", "INR" },
                { "receipt", paymentRequest.Receipt }
            };

            Order order = client.Order.Create(options);

           bool isusersubscribed= _dbContext.Subscriptions.Any(x => x.fk_userId == paymentRequest.StaffId);

            if(!isusersubscribed)
            {
                var subsc = new Models.Subscription();
                subsc.Amount = paymentRequest.Amount ;
                subsc.fk_userId = paymentRequest.StaffId;
                subsc.SubscriptionStartDate = DateTime.Now;
                subsc.SubscriptionEndDate = DateTime.Now.AddDays(365);
                _dbContext.Subscriptions.AddAsync(subsc);
                _dbContext.SaveChanges();
            }

            return Ok(new { orderId = order["id"].ToString() });
        }


        [HttpPost("verifyPayment")]
        public IActionResult VerifyPayment([FromBody] PaymentVerification paymentVerification)
        {
            var generatedSignature = RazorpayHelper.GenerateSignature(
                paymentVerification.razorpay_order_id + "|" + paymentVerification.razorpay_payment_id,
                _paymentsetting.PaymentSecret);

            if (generatedSignature == paymentVerification.razorpay_signature)
            {
                // Payment is successful
                return Ok(new { status = "Payment Successful" });
            }
            else
            {
                // Payment verification failed
                return BadRequest(new { status = "Payment Failed" });
            }
        }
    }
}
