﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class CustomUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public bool Isvalid { get; set; }
        public string PhoneNumber { get; set; }
        public bool isSubscribed { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
    }
}
