using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPaperApi.Models;

namespace TestPaperApi.Helper
{
    public class AttemptQuestionHelper
    {
        private DatabaseContext _dbContext;
        public AttemptQuestionHelper(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
        }
        public async Task<List<customStudentAttemptQuestion>> addQuestion(int attemptid, int subsubjectid)
        {
            var getallQuestion = await _dbContext.subjectQuestions.ToListAsync();

            var filterQuestions = getallQuestion.Where(x => x.fk_SubSubjectId == subsubjectid);

            if (filterQuestions.Count() > 0)
            {
                foreach (var que in filterQuestions)
                {
                    var newentity = new StudentAttemptQuestions();
                    newentity.fk_QuestionId = que.QuestionId;
                    newentity.fk_StudentAttemptId = attemptid;
                    newentity.MarkforReview = false;
                    newentity.NotAttempted = true;
                    newentity.selectedOption = null;

                    await _dbContext.StudentAttemptQuestions.AddAsync(newentity);
                }
                await _dbContext.SaveChangesAsync();
            }

            return await GetCustomQuestionsSet(attemptid);
        }

        public async Task<List<customStudentAttemptQuestion>> GetCustomQuestionsSet(int attemptid)
        {
            var dataobj = _dbContext.StudentAttemptQuestions.Join(_dbContext.StudentAttempts,
                    x => x.fk_StudentAttemptId, y => y.AttemptId,
                    (x, y) => new { x, y })
                    .Join(_dbContext.subjectQuestions, z => z.x.fk_QuestionId, k => k.QuestionId,
                    (z, k) => new { z, k })
                    .Where(q => q.z.y.AttemptId == attemptid)
                    .Select(e => new customStudentAttemptQuestion
                    {
                        fk_QuestionId = e.z.x.fk_QuestionId,
                        ImageQuestion = e.k.ImageQuestion,
                        MarkforReview = e.z.x.MarkforReview,
                        NotAttempted = e.z.x.NotAttempted,
                        Option1 = e.k.Option1,
                        Option2 = e.k.Option2,
                        Option3 = e.k.Option3,
                        Option4 = e.k.Option4,
                        Question = e.k.Question,
                        Answers = e.k.Answers,
                        selectedOption = e.z.x.selectedOption,
                        StudentAttemptQuestionId = e.z.x.StudentAttemptQuestionId,
                        fk_attemptId = attemptid
                    });

            return await dataobj.ToListAsync();
        }
    }
}
