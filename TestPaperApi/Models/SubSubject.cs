using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class SubSubject
    {
        [Key]
        public int SubSubjectId { get; set; }
        public int fk_SubjectId { get; set; }
        public int fk_userId { get; set; }
        public string SubSubjectName { get; set; }
        public Double Duration { get; set; }
        public int TotalMarks { get; set; }
        public bool isComplete { get; set; } //gives whether question writing is completed 
        public bool isVisible { get; set; } // whether it should be visible to student 
        public DateTime CreatedDatetine { get; set; }
    }
}
