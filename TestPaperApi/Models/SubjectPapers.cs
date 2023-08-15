using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class SubjectPapers
    {
        public int SubjectId { get; set; }
        public int fk_UserId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectLabel { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int PapersCount { get; set; }
    }
}
