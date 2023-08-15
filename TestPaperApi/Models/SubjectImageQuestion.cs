using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class SubjectImageQuestion
    {
        [Key]
        public int QuestionId { get; set; }
        public int fk_SubjectId { get; set; }
        public int fk_SubSubjectId { get; set; }
        public byte[] Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public bool IsMultipleChoice { get; set; }
        public int Marks { get; set; }
        public string Answers { get; set; }
        public int Order { get; set; }
    }
}
