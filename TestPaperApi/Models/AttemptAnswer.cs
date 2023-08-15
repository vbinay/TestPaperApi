using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class AttemptAnswer
    {
        [Key]
        public int AttemptAnswerId { get; set; }
        public int fk_AttemptId { get; set; }
        public int fk_QuestionId { get; set; }
        public string selectedOption { get; set; }

    }
}
