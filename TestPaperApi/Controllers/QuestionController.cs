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

        [HttpGet]
        public async Task<List<QuestionAll>> GetAllQuestions(int paperId)
        {
            List<QuestionAll> quesList = new List<QuestionAll>();

            var qList = await _dbContext.subjectQuestions.ToListAsync();
            var  qimglist = await _dbContext.SubjectImageQuestions.ToListAsync();

            if(qList.Any() && qimglist.Any())
            { 
                var qpaperlist = qList.Where(x => x.fk_SubSubjectId == paperId);
                var qimgpaperlist = qimglist.Where(x => x.fk_SubSubjectId == paperId);

                if(qpaperlist.Any() && qimgpaperlist.Any())
                {
                    foreach(var item in qpaperlist)
                    {
                        var pep = new QuestionAll();
                        pep.fk_SubjectId = item.fk_SubjectId;
                        pep.fk_SubSubjectId = item.fk_SubSubjectId;
                        pep.Answers = item.Answers;
                        pep.Option1 = item.Option1;
                        pep.Option2 = item.Option2;
                        pep.Option3 = item.Option3;
                        pep.Option4 = item.Option4;
                        pep.Option5 = item.Option5;
                        pep.Order = item.Order;
                        pep.QuestionId = item.QuestionId;
                        pep.textQuestion = item.Question;
                    }

                    foreach (var item in qimgpaperlist)
                    {
                        var pep = new QuestionAll();
                        pep.fk_SubjectId = item.fk_SubjectId;
                        pep.fk_SubSubjectId = item.fk_SubSubjectId;
                        pep.Answers = item.Answers;
                        pep.Option1 = item.Option1;
                        pep.Option2 = item.Option2;
                        pep.Option3 = item.Option3;
                        pep.Option4 = item.Option4;
                        pep.Option5 = item.Option5;
                        pep.Order = item.Order;
                        pep.QuestionId = item.QuestionId;
                        pep.imgQuestion = item.Question;
                    }
                }
            }

            return quesList.OrderBy(x=>x.Order).ToList();
        }
       
        [HttpPost("SaveText")]
        public async Task<ActionResult<string>> SaveText(SubjectQuestions question)
        {
            await _dbContext.subjectQuestions.AddAsync(question);
            _dbContext.SaveChanges();

            return Ok("Saved Successfully");
        }

        [HttpPost("SaveImage")]
        public async Task<ActionResult<string>> SaveImage(SubjectImageQuestion question)
        {
            await _dbContext.SubjectImageQuestions.AddAsync(question);
            _dbContext.SaveChanges();

            return Ok("Saved Successfully");
        }

        [HttpPost("UpdateText")]
        public async Task<ActionResult<string>> UpdateText(SubjectQuestions question)
        {
            var result = await _dbContext.subjectQuestions.FindAsync(question.QuestionId);

            if(result != null)
            {
                result.Question = question.Question;
                result.Order = question.Order;
                result.Option5 = question.Option5;
                result.Option4 = question.Option4;
                result.Option3 = question.Option3;
                result.Option2 = question.Option2;
                result.Option1 = question.Option1;
                result.Answers = question.Answers;
                result.fk_SubjectId = question.fk_SubjectId;
                result.fk_SubSubjectId = question.fk_SubSubjectId;
            }

            _dbContext.SaveChanges();
            return Ok("Updated Successfully");
        }

        [HttpPost("UpdateImage")]
        public async Task<ActionResult<string>> UpdateImage(SubjectImageQuestion question)
        {
            var result = await _dbContext.SubjectImageQuestions.FindAsync(question.QuestionId);

            if (result != null)
            {
                result.Question = question.Question;
                result.Order = question.Order;
                result.Option5 = question.Option5;
                result.Option4 = question.Option4;
                result.Option3 = question.Option3;
                result.Option2 = question.Option2;
                result.Option1 = question.Option1;
                result.Answers = question.Answers;
                result.fk_SubjectId = question.fk_SubjectId;
                result.fk_SubSubjectId = question.fk_SubSubjectId;
            }

            _dbContext.SaveChanges();
            return Ok("Updated Successfully");
        }
    }
}
