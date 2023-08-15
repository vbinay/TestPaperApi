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
    [Migration("20221202172124_coltotmarksadded")]
    partial class coltotmarksadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestPaperApi.Models.AttemptAnswer", b =>
                {
                    b.Property<int>("AttemptAnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("fk_AttemptId")
                        .HasColumnType("int");

                    b.Property<int>("fk_QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("selectedOption")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AttemptAnswerId");

                    b.ToTable("attemptAnswers");
                });

            modelBuilder.Entity("TestPaperApi.Models.StudentAttempt", b =>
                {
                    b.Property<int>("AttemptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("fk_SubSubjectId")
                        .HasColumnType("int");

                    b.Property<int>("fk_SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("fk_UserId")
                        .HasColumnType("int");

                    b.Property<bool>("isComplete")
                        .HasColumnType("bit");

                    b.HasKey("AttemptId");

                    b.ToTable("StudentAttempts");
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

                    b.Property<string>("Duration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubSubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalMarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("fk_SubjectId")
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

            modelBuilder.Entity("TestPaperApi.Models.SubjectImageQuestion", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<byte[]>("Question")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("fk_SubSubjectId")
                        .HasColumnType("int");

                    b.Property<int>("fk_SubjectId")
                        .HasColumnType("int");

                    b.HasKey("QuestionId");

                    b.ToTable("SubjectImageQuestions");
                });

            modelBuilder.Entity("TestPaperApi.Models.SubjectQuestions", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("fk_SubSubjectId")
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
