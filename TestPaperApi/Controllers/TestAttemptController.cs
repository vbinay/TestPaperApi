using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPaperApi.Helper;
using TestPaperApi.Models;

namespace TestPaperApi.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestAttemptController : ControllerBase
    {
        public readonly DatabaseContext _dbContext;
        public TestAttemptController(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
        }

        [HttpGet("GetStudentAttemptedTestsbyAttemptId")]
        public async Task<ActionResult<List<StudentAttemptSubSubjectJoin>>> GetStudentAttemptedTestsbyAttemptId(int attemptId)
        {
            var subs = await _dbContext.StudentAttempts.ToListAsync();

            if (subs.Any())
            {
                var dataobj = _dbContext.StudentAttempts.Join(_dbContext.subSubjects,
                    x => x.fk_SubSubjectId, y => y.SubSubjectId,
                    (x, y) => new { x, y })
                    .Where(x => x.x.AttemptId == attemptId)
                    .Select(e => new StudentAttemptSubSubjectJoin
                    {
                        AttemptId = e.x.AttemptId,
                        Duration = e.y.Duration,
                        fk_SubSubjectId = e.x.fk_SubSubjectId,
                        fk_UserId = e.x.fk_UserId,
                        isComplete = e.x.isComplete,
                        isContinue = e.x.isContinue,
                        SubSubjectName = e.y.SubSubjectName,
                        TotalMarks = e.y.TotalMarks
                    });

                return await dataobj.ToListAsync();

            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("GetStudentAttemptedTestsbyStudentId")]
        public async Task<ActionResult<List<StudentAttemptSubSubjectJoin>>> GetStudentAttemptedTestsbyStudentId(int userId)
        {
            var subs = await _dbContext.StudentAttempts.ToListAsync();

            if (subs.Any())
            {
                var dataobj = _dbContext.StudentAttempts.Join(_dbContext.subSubjects,
                    x => x.fk_SubSubjectId, y => y.SubSubjectId,
                    (x, y) => new { x, y })
                    .Where(x => x.x.fk_UserId == userId)
                    .Select(e => new StudentAttemptSubSubjectJoin
                    {
                        AttemptId = e.x.AttemptId,
                        Duration = e.y.Duration,
                        fk_SubSubjectId = e.x.fk_SubSubjectId,
                        fk_UserId = e.x.fk_UserId,
                        isComplete = e.x.isComplete,
                        isContinue = e.x.isContinue,
                        SubSubjectName = e.y.SubSubjectName,
                        TotalMarks = e.y.TotalMarks
                    });

                return await dataobj.ToListAsync();
                
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetStudentAttemptedTestsbyTestId")]
        public async Task<ActionResult<List<StudentAttemptSubSubjectJoin>>> GetStudentAttemptedTestsbyTestId(int subsubjectId)
        {
            var subs = await _dbContext.StudentAttempts.ToListAsync();

            if (subs.Any())
            {
                var dataobj = _dbContext.StudentAttempts.Join(_dbContext.subSubjects,
                    x => x.fk_SubSubjectId, y => y.SubSubjectId,
                    (x, y) => new { x, y })
                    .Where(x => x.x.fk_SubSubjectId == subsubjectId)
                    .Select(e => new StudentAttemptSubSubjectJoin
                    {
                        AttemptId = e.x.AttemptId,
                        Duration = e.y.Duration,
                        fk_SubSubjectId = e.x.fk_SubSubjectId,
                        fk_UserId = e.x.fk_UserId,
                        isComplete = e.x.isComplete,
                        isContinue = e.x.isContinue,
                        SubSubjectName = e.y.SubSubjectName,
                        TotalMarks = e.y.TotalMarks
                    });

                return await dataobj.ToListAsync();

            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("AddStudentAttempt")]
        public async Task<ActionResult<StudentAttempt>> AddStudentAttempt(StudentAttempt studentAttempt)
        {
            StudentAttempt studAttempt = new StudentAttempt();
            studAttempt.fk_SubSubjectId = studentAttempt.fk_SubSubjectId;
            studAttempt.fk_UserId = studentAttempt.fk_UserId;
            studAttempt.isComplete = false;
            studAttempt.isContinue = true;

            await _dbContext.StudentAttempts.AddAsync(studAttempt);
            await _dbContext.SaveChangesAsync();

            var getltestid =await _dbContext.StudentAttempts.OrderByDescending(x=>x.AttemptId).FirstAsync();

            var stuattemptques = new AttemptQuestionHelper(_dbContext);
            var questions = await stuattemptques.addQuestion(getltestid.AttemptId, studentAttempt.fk_SubSubjectId);

            return getltestid;
        }

        [HttpPost("UpdateStudentAttempt")]
        public async Task<ActionResult<string>> UpdateStudentAttempt(StudentAttempt studentAttempt)
        {
            var studsttempt = await _dbContext.StudentAttempts.FindAsync(studentAttempt.AttemptId);

            if(studsttempt != null)
            {
                studsttempt.fk_SubSubjectId = studentAttempt.fk_SubSubjectId;
                studsttempt.fk_UserId = studentAttempt.fk_UserId;
                studsttempt.isComplete = studentAttempt.isComplete;
                studsttempt.isContinue = studentAttempt.isContinue;
            }

             _dbContext.StudentAttempts.Update(studsttempt);
            await _dbContext.SaveChangesAsync();

            return Ok("Updated the StudentAttempt");

        }

    }
}
