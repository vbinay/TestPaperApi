﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPaperApi.Models;

namespace TestPaperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly DatabaseContext _dbContext;
        public UserController(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUser()
        {
            return await _dbContext.Users.ToListAsync();
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("ValidateUser")]
        public async Task<ActionResult<Users>> ValidateUser(string username, string password)
        {
            if(!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                var allUsers = await _dbContext.Users.ToListAsync();

                string pwd = encodepassword(password);
                var selectedUser= allUsers
                    .Where(x => x.UserName == username || x.Email==username && x.Password == pwd);

                if(selectedUser.Any())
                {
                    return selectedUser.FirstOrDefault();
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
        [HttpPost]
        public async Task<ActionResult<Users>> SaveUser(Users user)
        {
            var UserToAdd = user;
            if(string.IsNullOrWhiteSpace(UserToAdd.UserType))
            {
                UserToAdd.UserType = "student";
            }

            var addedUsers =await  _dbContext.Users.ToListAsync();

            if(addedUsers.Any(x=>x.UserName==user.UserName && x.Email==user.Email))
            {
                throw new Exception("Duplicate Record Exist");
            }

            if(string.IsNullOrWhiteSpace(UserToAdd.Password))
            {
                throw new Exception("Password Empty");
            }

            UserToAdd.Password = encodepassword(UserToAdd.Password);

            await _dbContext.Users.AddAsync(UserToAdd);
            await  _dbContext.SaveChangesAsync();
            return Ok(UserToAdd);
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
