﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestPaperApi.Models;

namespace TestPaperApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240120184417_attemptimeadded")]
    partial class attemptimeadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestPaperApi.Models.StudentAttempt", b =>
                {
                    b.Property<int>("AttemptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AttemptTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("fk_SubSubjectId")
                        .HasColumnType("int");

                    b.Property<int>("fk_UserId")
                        .HasColumnType("int");

                    b.Property<bool>("isComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("isContinue")
                        .HasColumnType("bit");

                    b.HasKey("AttemptId");

                    b.ToTable("StudentAttempts");
                });

            modelBuilder.Entity("TestPaperApi.Models.StudentAttemptQuestions", b =>
                {
                    b.Property<int>("StudentAttemptQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("MarkforReview")
                        .HasColumnType("bit");

                    b.Property<bool>("NotAttempted")
                        .HasColumnType("bit");

                    b.Property<int>("fk_QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("fk_StudentAttemptId")
                        .HasColumnType("int");

                    b.Property<string>("selectedOption")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentAttemptQuestionId");

                    b.ToTable("StudentAttemptQuestions");
                });

            modelBuilder.Entity("TestPaperApi.Models.StudentResult", b =>
                {
                    b.Property<int>("ResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Answers")
                        .HasColumnType("int");

                    b.Property<int>("CorrectAnswers")
                        .HasColumnType("int");

                    b.Property<int>("fk_SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("fk_UserId")
                        .HasColumnType("int");

                    b.Property<int>("fk_attemptId")
                        .HasColumnType("int");

                    b.Property<int>("fk_subSubjectId")
                        .HasColumnType("int");

                    b.HasKey("ResultId");

                    b.ToTable("StudentResults");
                });

            modelBuilder.Entity("TestPaperApi.Models.SubSubject", b =>
                {
                    b.Property<int>("SubSubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDatetine")
                        .HasColumnType("datetime2");

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<string>("SubSubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalMarks")
                        .HasColumnType("int");

                    b.Property<int>("fk_SubjectGroupId")
                        .HasColumnType("int");

                    b.Property<int>("fk_userId")
                        .HasColumnType("int");

                    b.Property<bool>("isComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("isVisible")
                        .HasColumnType("bit");

                    b.HasKey("SubSubjectId");

                    b.ToTable("subSubjects");
                });

            modelBuilder.Entity("TestPaperApi.Models.SubjectGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubjectGroupId")
                        .HasColumnType("int");

                    b.Property<string>("SubjectGroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("fk_SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("fk_UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("subjectGroup");
                });

            modelBuilder.Entity("TestPaperApi.Models.SubjectQuestions", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DifficultyLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageQuestion")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsMultipleChoice")
                        .HasColumnType("bit");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("Option1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("fk_SubSubjectId")
                        .HasColumnType("int");

                    b.Property<int>("fk_SubjectId")
                        .HasColumnType("int");

                    b.HasKey("QuestionId");

                    b.ToTable("subjectQuestions");
                });

            modelBuilder.Entity("TestPaperApi.Models.Subjects", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubjectLabel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("fk_UserId")
                        .HasColumnType("int");

                    b.HasKey("SubjectId");

                    b.ToTable("subjects");
                });

            modelBuilder.Entity("TestPaperApi.Models.Subscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("SubscriptionEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SubscriptionStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("fk_userId")
                        .HasColumnType("int");

                    b.HasKey("SubscriptionId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("TestPaperApi.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
