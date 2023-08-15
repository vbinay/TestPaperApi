using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPaperApi.Migrations
{
    public partial class updatequestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMultipleChoice",
                table: "subjectQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Marks",
                table: "subjectQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMultipleChoice",
                table: "SubjectImageQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Marks",
                table: "SubjectImageQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMultipleChoice",
                table: "subjectQuestions");

            migrationBuilder.DropColumn(
                name: "Marks",
                table: "subjectQuestions");

            migrationBuilder.DropColumn(
                name: "IsMultipleChoice",
                table: "SubjectImageQuestions");

            migrationBuilder.DropColumn(
                name: "Marks",
                table: "SubjectImageQuestions");
        }
    }
}
