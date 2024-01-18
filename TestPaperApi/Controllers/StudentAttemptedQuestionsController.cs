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
    public class StudentAttemptedQuestionsController : Controller
    {
        public readonly DatabaseContext _dbContext;
        public readonly AttemptQuestionHelper _helper;
        public StudentAttemptedQuestionsController(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
            this._helper = new AttemptQuestionHelper(this._dbContext);
        }

        [HttpGet("AttemptedTestQuestionsbyAttemptId")]
        public async Task<ActionResult<List<customStudentAttemptQuestion>>> GetStudentAttemptedTestsbyAttemptId(int attemptId)
        {
            var subs = await _dbContext.StudentAttemptQuestions.ToListAsync();

            if (subs.Any())
            {
               return await _helper.GetCustomQuestionsSet(attemptId);
            }
            else
            {
                return NotFound(null);
            }
        }

        [HttpPost("AddQuestionToStudentAttempt")]
        public async Task<ActionResult<List<customStudentAttemptQuestion>>> AddQuestionToStudentAttempt(int attemptid, int subsubjectid)
        {
            return await _helper.addQuestion(attemptid, subsubjectid);
        }


        [HttpPost("UpdateQuestionToStudentAttempt")]
        public async Task<ActionResult<List<customStudentAttemptQuestion>>> UpdateQuestionToStudentAttempt(customStudentAttemptQuestion questiondata)
        {
            if(questiondata.fk_attemptId>0 && questiondata.StudentAttemptQuestionId==0  && questiondata.fk_QuestionId==0)
            {
                return await _helper.GetCustomQuestionsSet(questiondata.fk_attemptId);
            }
            else
            {
                var getQuestion = await _dbContext.StudentAttemptQuestions.FindAsync(questiondata.StudentAttemptQuestionId);

                if (getQuestion != null)
                {
                    getQuestion.MarkforReview = questiondata.MarkforReview;
                    getQuestion.NotAttempted = questiondata.NotAttempted;
                    getQuestion.selectedOption = questiondata.selectedOption;

                    await _dbContext.SaveChangesAsync();
                }

                return await _helper.GetCustomQuestionsSet(questiondata.fk_attemptId);
            }
        }


        [HttpPost("BulkUpdateQuestionToStudentAttempt")]
        public async Task<ActionResult<string>> BulkUpdateQuestionToStudentAttempt(customStudentAttemptQuestion[] questiondata)
        {
            foreach (var que in questiondata)
            {
                var getQuestion = await _dbContext.StudentAttemptQuestions.FindAsync(que.StudentAttemptQuestionId);

                if (getQuestion != null)
                {
                    getQuestion.MarkforReview = que.MarkforReview;
                    getQuestion.NotAttempted = que.NotAttempted;
                    getQuestion.selectedOption = que.selectedOption;
                }
            }

            await _dbContext.SaveChangesAsync();
            return Ok("Updated Questions Succesfully");
        }

    }
}
