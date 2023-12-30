using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class SubjectQuestions
    {
        [Key]
        public int QuestionId { get; set; }
        public int fk_SubjectId { get; set; }
        public int fk_SubSubjectId { get; set; }
        public byte[] ImageQuestion { get; set; }
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public bool IsMultipleChoice { get; set; }
        public int Marks { get; set; }
        public string Answers { get; set; }
        public int Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DifficultyLevel { get; set; }

    }
}
