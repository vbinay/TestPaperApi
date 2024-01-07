using System;
using System.ComponentModel.DataAnnotations;

namespace TestPaperApi.Models
{
    public class SubjectGroup
    {
        [Key]
        public int Id { get; set; }
        public int SubjectGroupId { get; set; }
        public int fk_UserId { get; set; }
        public int fk_SubjectId { get; set; }
        public string SubjectGroupName { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}