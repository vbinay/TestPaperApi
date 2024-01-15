using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class StudentAttemptQuestions
    {
        [Key]
        public int StudentAttemptQuestionId { get; set; }
        public int fk_StudentAttemptId { get; set; }
        public int fk_QuestionId { get; set; }
        public string selectedOption { get; set; }
        public bool MarkforReview { get; set; }
        public bool NotAttempted { get; set; }
    }
}
