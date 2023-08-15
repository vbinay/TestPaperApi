﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPaperApi.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<SubjectImageQuestion> SubjectImageQuestions { get; set; }
        public DbSet<SubjectQuestions> subjectQuestions { get; set; }
        public DbSet<Subjects> subjects { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<AttemptAnswer> attemptAnswers { get; set; }
        public DbSet<StudentAttempt> StudentAttempts { get; set; }
        public DbSet<StudentResult> StudentResults { get; set; }
        public DbSet<SubSubject> subSubjects { get; set; }

    }
}