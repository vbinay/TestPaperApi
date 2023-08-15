using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

                var selectedUser= allUsers
                    .Where(x => x.UserName == username && x.Password == password);

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
            var addedUsers =await  _dbContext.Users.ToListAsync();

            if(addedUsers.Any(x=>x.UserName==user.UserName && x.Email==user.Email))
            {
                throw new Exception("Duplicate Record Exist");
            }
            await _dbContext.Users.AddAsync(UserToAdd);
            await  _dbContext.SaveChangesAsync();
            return Ok(UserToAdd);
        }
    }
    
    public enum UserType
    {
        Admin ,
        Student
    }
}
