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
    public class TestQuestionsController:Controller
    {
        public readonly DatabaseContext _dbContext;
        public TestQuestionsController(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
        }


        [HttpGet("GetallTestQuestionByTestId")]
        public async Task<ActionResult<List<SubjectQuestions>>> GetallTestQuestionByTestId(int testid)
        {

            var subs = await _dbContext.subSubjects.FindAsync(testid);
            var subquestions = await _dbContext.subjectQuestions.ToListAsync();

            if (subs != null && subs.SubSubjectId>0)
            {
                if(subquestions.Any())
                {
                    var filterquestion = subquestions.Where(x => x.fk_SubSubjectId == testid);
                    if(filterquestion != null && filterquestion.Count()>0)
                    {
                        return filterquestion.ToList();
                    }
                    else
                    {
                        return NotFound("No Questions Added");
                    }
                }
            }

            return NotFound("No Records Found");
        }

        [HttpGet("GetallTestQuestionByTestName")]
        public async Task<ActionResult<List<SubjectQuestions>>> GetallTestQuestionByTestName(string testname)
        {

            var subs = await _dbContext.subSubjects.FirstOrDefaultAsync(x=>x.SubSubjectName==testname);
            var subquestions = await _dbContext.subjectQuestions.ToListAsync();

            if (subs != null && subs.SubSubjectId > 0)
            {
                if (subquestions.Any())
                {
                    var filterquestion = subquestions.Where(x => x.fk_SubSubjectId == subs.SubSubjectId);
                    if (filterquestion != null && filterquestion.Count() > 0)
                    {
                        return filterquestion.ToList();
                    }
                    else
                    {
                        return NotFound("No Questions Added");
                    }
                }
            }

            return NotFound("No Records Found");
        }
    }
}
