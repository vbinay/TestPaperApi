using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }
        public int fk_userId { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public Double  Amount { get; set; }
    }
}
