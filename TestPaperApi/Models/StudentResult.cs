using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class StudentResult
    {
        [Key]
        public int ResultId { get; set; }
        public int fk_UserId { get; set; }
        public int fk_SubjectId { get; set; }
        public int fk_subSubjectId { get; set; }
        public int fk_attemptId { get; set; }
        public int Answers { get; set; }
        public int CorrectAnswers { get; set; }

    }
}
