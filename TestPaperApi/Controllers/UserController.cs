using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPaperApi.Models;
using TestPaperApi.Services;

namespace TestPaperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly DatabaseContext _dbContext;
        private readonly IMailService mailService;
        public UserController(DatabaseContext databaseContext, IMailService mailService)
        {
            this._dbContext = databaseContext;
            this.mailService = mailService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUser()
        {
            return await _dbContext.Users.ToListAsync();
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("ValidateUser")]
        public async Task<ActionResult<CustomUser>> ValidateUser(string username, string password)
        {
            if(!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                var allUsers = await _dbContext.Users.ToListAsync();

                string pwd = encodepassword(password);
                var selectedUser= allUsers
                    .Where(x => x.UserName == username || x.Email==username && x.Password == pwd);

                if(selectedUser.Any())
                {
                    var userr = new CustomUser();
                    userr.Email = selectedUser.FirstOrDefault().Email;
                    userr.FirstName = selectedUser.FirstOrDefault().FirstName;
                    userr.Gender = selectedUser.FirstOrDefault().Gender;
                    userr.IsActive = selectedUser.FirstOrDefault().IsActive;
                    userr.Isvalid = selectedUser.FirstOrDefault().Isvalid;
                    userr.LastName = selectedUser.FirstOrDefault().LastName;
                    userr.PhoneNumber = selectedUser.FirstOrDefault().PhoneNumber;
                    userr.UserId = selectedUser.FirstOrDefault().UserId;
                    userr.UserName= selectedUser.FirstOrDefault().UserName;
                    userr.UserType = selectedUser.FirstOrDefault().UserType;

                    var ressub = _dbContext.Subscriptions.Where(x => x.fk_userId == selectedUser.FirstOrDefault().UserId);
                    if(ressub != null && ressub.Count()>0)
                    {
                        userr.isSubscribed = true;
                        userr.SubscriptionStartDate = ressub.First().SubscriptionStartDate;
                    }
                    return userr;
                }
                else
                {
                    return NotFound();
                }
            }
           else
            {
                return BadRequest();
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("ActivateUserEmailLink")]
        public async Task<ActionResult<string>> ActivateUserEmailLink(string UniqueCode)
        {
            if (!string.IsNullOrWhiteSpace(UniqueCode))
            {
                var selectedUser = await _dbContext.Users.Where(x=>x.ActivationLink== UniqueCode).FirstOrDefaultAsync();

                if (selectedUser != null)
                {
                    selectedUser.Isvalid = true;
                    _dbContext.Users.Update(selectedUser);
                    await _dbContext.SaveChangesAsync();
                    return Redirect("https://testworld.co.in/login");
                }
            }
            else
            {
                throw new Exception("Invalid Activation code Sent");
            }
            return null;
        }

        [EnableCors("AllowOrigin")]
        [HttpPost("SaveUser")]
        public async Task<ActionResult<Users>> SaveUser(Users user)
        {
            var UserToAdd = user;
            if (string.IsNullOrWhiteSpace(UserToAdd.UserType))
            {
                UserToAdd.UserType = "student";
            }
            var addedUsers = await _dbContext.Users.ToListAsync();

            if (addedUsers.Any(x => x.PhoneNumber == user.PhoneNumber && x.Email == user.Email))
            {
                throw new Exception("Duplicate Record Exist");
            }

            if (string.IsNullOrWhiteSpace(UserToAdd.Password))
            {
                throw new Exception("Password Empty");
            }

            UserToAdd.Isvalid = false;
            UserToAdd.Password = encodepassword(UserToAdd.Password);
            var unq = Guid.NewGuid().ToString();
            UserToAdd.ActivationLink = unq;

            string createurl = "https://api.testworld.co.in/api/User/ActivateUserEmailLink?UniqueCode=" + unq;

            var mailreq = new MailRequest();
            mailreq.ToEmail = UserToAdd.Email;
            mailreq.Subject = "Activation Link";
            mailreq.Body = createurl;

            await this.mailService.SendEmailAsync(mailreq);

            await _dbContext.Users.AddAsync(UserToAdd);
            await _dbContext.SaveChangesAsync();
            return Ok(UserToAdd);
        }

        [EnableCors("AllowOrigin")]
        [HttpPost("UpdateUser")]
        public async Task<ActionResult<Users>> UpdateUser(Users user)
        {

            var addedUsers = _dbContext.Users.Where(x => x.UserName == user.UserName && x.Email == user.Email);

            if (addedUsers.Any())
            {
                addedUsers.First().Email = user.Email;
                addedUsers.First().FirstName = user.FirstName;
                addedUsers.First().Gender = user.Gender;
                addedUsers.First().IsActive = user.IsActive;
                addedUsers.First().LastName = user.LastName;
                addedUsers.First().PhoneNumber = user.PhoneNumber;
                addedUsers.First().UserName = user.UserName;
                addedUsers.First().UserType = user.UserType;
                addedUsers.First().Isvalid = user.Isvalid;
            }

            await _dbContext.SaveChangesAsync();
            return Ok("Updated");
        }

        [EnableCors("AllowOrigin")]
        [HttpDelete]
        public async Task<ActionResult<Users>> SaveUser(string  emailId)
        {
            var addedUsers = await _dbContext.Users.ToListAsync();
            var resultset = addedUsers.Where(x => x.Email == emailId);

            if(resultset != null)
            {
                _dbContext.Users.RemoveRange(resultset);
            }

            await _dbContext.SaveChangesAsync();
            return Ok("User Deleted Successfully");
        }

        private string encodepassword(string password)
        {
            var data = Encoding.UTF8.GetBytes(password);
            //return as base64 string
            return Convert.ToBase64String(data);
        }

        private string decryptpassword(string password)
        {
            var str= Convert.FromBase64String(password);
            return Encoding.UTF8.GetString(str);
        }
    }
}
