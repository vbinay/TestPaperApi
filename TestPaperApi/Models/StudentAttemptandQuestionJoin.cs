using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class StudentAttemptandQuestionJoin
    {
        public int AttemptId { get; set; }
        public int fk_UserId { get; set; }
        public int fk_SubSubjectId { get; set; }
        public bool isComplete { get; set; }
        public bool isContinue { get; set; }
        public List<customStudentAttemptQuestion> QuestionforTest { get; set; }

    }

    public class customStudentAttemptQuestion
    {
        public int StudentAttemptQuestionId { get; set; }
        public int fk_attemptId { get; set; }
        public int fk_QuestionId { get; set; }
        public byte[] ImageQuestion { get; set; }
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string selectedOption { get; set; }
        public bool MarkforReview { get; set; }
        public bool NotAttempted { get; set; }
    }
}
