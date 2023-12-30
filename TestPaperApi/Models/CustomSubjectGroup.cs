using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class CustomSubjectGroup
    {
        public int SubjectGroupId { get; set; }
        public int fk_UserId { get; set; }
        public List<int> SubjectIds { get; set; }
        public string SubjectGroupName { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
