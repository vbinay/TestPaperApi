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

        [HttpGet("getpaperdetail")]
        public async Task<PaperQuestions>  GetPaperDetail(int paperId)
        {
            var pepques = new PaperQuestions();
            if(paperId>0)
            {
                var pep = await _dbContext.subSubjects.FirstAsync(x => x.SubSubjectId == paperId);
                var questionslist = await _dbContext.subjectQuestions.ToListAsync();

                pepques.SubSubjectId = pep.SubSubjectId;
                pepques.SubSubjectName = pep.SubSubjectName;
                pepques.TotalMarks = pep.TotalMarks;
                pepques.Duration = pep.Duration;
                pepques.fk_SubjectId = pep.fk_SubjectId;
                pepques.fk_userId = pep.fk_userId;
                pepques.isComplete = pep.isComplete;
                pepques.isVisible = pep.isVisible;
                int cnt1 = questionslist.Count(x => x.fk_SubSubjectId == paperId);

                pepques.QuestionCount = cnt1;
            }

            return pepques;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaperQuestions>>> GetAllPapers(string subjectId)
        {
            List<PaperQuestions> paperslst = new List<PaperQuestions>();
            if (!string.IsNullOrWhiteSpace(subjectId))
            {
                var allpapers = await _dbContext.subSubjects.ToListAsync();

                var selectedpapers = allpapers
                    .Where(x => x.fk_SubjectId==Convert.ToInt32(subjectId));

                var questionslist = await _dbContext.subjectQuestions.ToListAsync();

                foreach (var sub in allpapers)
                {
                    PaperQuestions pepQues = new PaperQuestions();
                    pepQues.Duration = sub.Duration;
                    pepQues.fk_SubjectId = sub.fk_SubjectId;
                    pepQues.fk_userId = sub.fk_userId;
                    pepQues.isComplete = sub.isComplete;
                    pepQues.isVisible = sub.isVisible;
                    pepQues.SubSubjectId = sub.SubSubjectId;
                    pepQues.SubSubjectName = sub.SubSubjectName;
                    pepQues.TotalMarks = sub.TotalMarks;

                    int cnt1 = questionslist.Count(x => x.fk_SubSubjectId == sub.SubSubjectId);

                    pepQues.QuestionCount = cnt1;

                    paperslst.Add(pepQues);
                }
            }

            return paperslst;
        }


        [HttpPost]
        public async Task<ActionResult<string>> SavePaper(SubSubject paper)
        {
            var addedpapers = await _dbContext.subSubjects.ToListAsync();

            if (addedpapers.Any(x => x.fk_SubjectId == paper.fk_SubjectId && x.SubSubjectName == paper.SubSubjectName))
            {
                return NoContent();
            }
            await _dbContext.subSubjects.AddAsync(paper);
            await _dbContext.SaveChangesAsync();
            return Ok("Saved Succesfully");
        }

        [HttpDelete]
        public async Task<ActionResult<string>> deletepaper(int paperid)
        { 
            var addedpapers = await _dbContext.subSubjects.ToListAsync();

            if (addedpapers.Any())
            {
                var selectedone = addedpapers.Where(x => x.SubSubjectId == paperid);
                 _dbContext.subSubjects.Remove(selectedone.First());
                await _dbContext.SaveChangesAsync();
            }
            return Ok("Deleted");
        }


        [HttpPost("updatepaper")]
        public async Task<ActionResult<string>> UpdatePaper(SubSubject paper)
        {
            var addedpapers = await _dbContext.subSubjects.ToListAsync();

            if (addedpapers.Any(x => x.SubSubjectId==paper.SubSubjectId))
            {
                addedpapers.First().isComplete = paper.isComplete;
                addedpapers.First().isVisible = paper.isVisible;
                await _dbContext.SaveChangesAsync();
                return Ok("Updated Succesfully");
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
