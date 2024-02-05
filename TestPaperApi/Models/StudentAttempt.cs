using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class StudentAttempt
    {
        [Key]
        public int AttemptId { get; set; }
        public int fk_UserId { get; set; }
        public int fk_SubSubjectId { get; set; }
        public bool isComplete { get; set; }
        public bool isContinue { get; set; }
        public string AttemptTime { get; set; }
    }
}
