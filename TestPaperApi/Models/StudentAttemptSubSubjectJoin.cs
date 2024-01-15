using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class StudentAttemptSubSubjectJoin
    {
        public int AttemptId { get; set; }
        public int fk_UserId { get; set; }
        public int fk_SubSubjectId { get; set; }
        public string SubSubjectName { get; set; }
        public Double Duration { get; set; }
        public int TotalMarks { get; set; }
        public bool isComplete { get; set; }
        public bool isContinue { get; set; }
    }
}
