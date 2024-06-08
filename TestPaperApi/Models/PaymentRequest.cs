using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class PaymentRequest
    {
        public int StaffId { get; set; }
        public int Amount { get; set; }
        public string Receipt { get; set; }
    }

    public class PaymentVerification
    {
        public string razorpay_payment_id { get; set; }
        public string razorpay_order_id { get; set; }
        public string razorpay_signature { get; set; }
    }
}
