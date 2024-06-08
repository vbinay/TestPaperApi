using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPaperApi.Migrations
{
    public partial class addedNegativeMarkandtopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NegativeMarking",
                table: "subSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "subjectQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubjectName",
                table: "subjectQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "subjectQuestions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NegativeMarking",
                table: "subSubjects");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "subjectQuestions");

            migrationBuilder.DropColumn(
                name: "SubjectName",
                table: "subjectQuestions");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "subjectQuestions");
        }
    }
}
