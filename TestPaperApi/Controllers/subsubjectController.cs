using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPaperApi.Models;

namespace TestPaperApi.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class subsubjectController : Controller
    {
        public readonly DatabaseContext _dbContext;
        public subsubjectController(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
        }

        [HttpGet("GetallTests")]
        public async Task<List<SubSubject>> GetallTests()
        {
            var subs = await _dbContext.subSubjects.ToListAsync();

            if (subs.Any())
            {
                return subs;
            }
            else
            {
                return new List<SubSubject>();
            }
        }


        [HttpGet("GetTestbyId")]
        public async Task<ActionResult<SubSubject>> GetTestById(int subsubjectid)
        {
            var subs = await _dbContext.subSubjects.ToListAsync();
            if (subs.Any())
            {
                var subfirst = subs.Where(x => x.SubSubjectId == subsubjectid);
                if(subfirst.Any())
                {
                    return subfirst.FirstOrDefault();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetTestbyName")]
        public async Task<ActionResult<SubSubject>> GetTestbyName(string subsubjectname)
        {
            var subs = await _dbContext.subSubjects.ToListAsync();
            if (subs.Any())
            {
                var subfirst = subs.Where(x => x.SubSubjectName == subsubjectname);
                if (subfirst.Any())
                {
                    return subfirst.FirstOrDefault();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NoContent();
            }
        }


        [HttpPost]
        public async Task<ActionResult<SubSubject>> SaveTest(SubSubject testdetail)
        {
            var testavailable = await _dbContext.subSubjects.ToListAsync();

            if(testavailable.Any())
            {
                var resultone = testavailable.Where(x => x.SubSubjectId == testdetail.SubSubjectId);
                if(resultone.Any())
                {
                    resultone.First().CreatedDatetine = testdetail.CreatedDatetine;
                    resultone.First().Duration = testdetail.Duration;
                    resultone.First().fk_SubjectGroupId = testdetail.fk_SubjectGroupId;
                    resultone.First().fk_userId = testdetail.fk_userId;
                    resultone.First().isComplete = testdetail.isComplete;
                    resultone.First().isVisible = testdetail.isVisible;
                    resultone.First().SubSubjectName = testdetail.SubSubjectName;
                }
                else
                {
                    await _dbContext.subSubjects.AddAsync(testdetail);
                }
            }
            
            await _dbContext.SaveChangesAsync();
            return Ok("Saved Succesfully");
        }

        [HttpDelete]
        public async Task<ActionResult<string>> deletepaper(int subsubjectid)
        { 
            var addedpapers = await _dbContext.subSubjects.ToListAsync();

            if (addedpapers.Any())
            {
                var selectedone = addedpapers.Where(x => x.SubSubjectId == subsubjectid);
                 _dbContext.subSubjects.Remove(selectedone.First());
                await _dbContext.SaveChangesAsync();
            }
            return Ok("Deleted");
        }
    }
}
