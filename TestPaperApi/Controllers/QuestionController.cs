using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class QuestionController : Controller
    {
        public readonly DatabaseContext _dbContext;
        public QuestionController(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
        }

        [HttpGet("GetAllQuestionsbyTestId")]
        public async Task<List<QuestionAll>> GetAllQuestionsbyTestId(int testid)
        {
            List<QuestionAll> quesList = new List<QuestionAll>();

            var qList =await _dbContext.subjectQuestions.Where(x => x.fk_SubSubjectId == testid).ToListAsync();

            if (qList.Any())
            {
                foreach (var item in qList)
                {
                    var pep = new QuestionAll();
                    pep.fk_SubjectId = item.fk_SubjectId;
                    pep.fk_SubSubjectId = item.fk_SubSubjectId > 0 ? item.fk_SubSubjectId : pep.fk_SubSubjectId;
                    pep.imgQuestion = item.ImageQuestion;
                    pep.Answers = item.Answers;
                    pep.Option1 = item.Option1;
                    pep.Option2 = item.Option2;
                    pep.Option3 = item.Option3;
                    pep.Option4 = item.Option4;
                    pep.Explanation = item.Explanation;
                    pep.Order = item.Order;
                    pep.QuestionId = item.QuestionId;
                    pep.textQuestion = item.Question;

                    quesList.Add(pep);
                }
            }

            return quesList.OrderBy(x=>x.Order).ToList();
        }

        [HttpGet("GetQuestionbyQuestionId")]
        public async Task<ActionResult<SubjectQuestions>> GetQuestionbyQuestionId(int questionid)
        {
            var result = await _dbContext.subjectQuestions.FindAsync(questionid);

            if (result != null)
            {
                return result;
            }
            else
            {
                return NotFound("Question Not Found");
            }
        }


        [HttpPost("SaveTestQuestion")]
        public async Task<ActionResult<string>> SaveTestQuestion(SubjectQuestions question)
        {
            await _dbContext.subjectQuestions.AddAsync(question);
            _dbContext.SaveChanges();

            return Ok("Saved Successfully");
        }

        [HttpPost("SaveallQuestion")]
        public async Task<ActionResult<string>> SaveallQuestion(List<SubjectQuestions> question)
        {
            await _dbContext.subjectQuestions.AddRangeAsync(question);
            _dbContext.SaveChanges();

            return Ok("Saved Successfully");
        }


        [HttpPost("UpdateTestQuestion")]
        public async Task<ActionResult<string>> UpdateTestQuestion(SubjectQuestions question)
        {
            var result = await _dbContext.subjectQuestions.FindAsync(question.QuestionId);

            if(result != null)
            {
                result.Question = question.Question;
                result.ImageQuestion = question.ImageQuestion;
                result.Order = question.Order;
                result.Option4 = question.Option4;
                result.Option3 = question.Option3;
                result.Option2 = question.Option2;
                result.Option1 = question.Option1;
                result.Answers = question.Answers;
                result.Explanation = question.Explanation;
                result.fk_SubjectId = question.fk_SubjectId;
                result.fk_SubSubjectId = question.fk_SubSubjectId;
                result.DifficultyLevel = question.DifficultyLevel;
            }

            _dbContext.SaveChanges();
            return Ok("Updated Successfully");
        }

        [HttpDelete("DeleteTestQuestion")]
        public async Task<ActionResult<string>> DeleteTestQuestion(int questionid)
        {
            var result = await _dbContext.subjectQuestions.FindAsync(questionid);

            if (result != null)
            {
                _dbContext.subjectQuestions.Remove(result);
                _dbContext.SaveChanges();
            }
            else
            {
                return NotFound("Question Not Found");
            }

            return Ok("Updated Successfully");
        }
    }
}
