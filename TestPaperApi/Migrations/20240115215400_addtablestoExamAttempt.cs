using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPaperApi.Migrations
{
    public partial class addtablestoExamAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attemptAnswers");

            migrationBuilder.DropColumn(
                name: "fk_SubjectId",
                table: "StudentAttempts");

            migrationBuilder.AddColumn<bool>(
                name: "isContinue",
                table: "StudentAttempts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "StudentAttemptQuestions",
                columns: table => new
                {
                    StudentAttemptQuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_StudentAttemptId = table.Column<int>(type: "int", nullable: false),
                    fk_QuestionId = table.Column<int>(type: "int", nullable: false),
                    selectedOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarkforReview = table.Column<bool>(type: "bit", nullable: false),
                    NotAttempted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttemptQuestions", x => x.StudentAttemptQuestionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAttemptQuestions");

            migrationBuilder.DropColumn(
                name: "isContinue",
                table: "StudentAttempts");

            migrationBuilder.AddColumn<int>(
                name: "fk_SubjectId",
                table: "StudentAttempts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "attemptAnswers",
                columns: table => new
                {
                    AttemptAnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_AttemptId = table.Column<int>(type: "int", nullable: false),
                    fk_QuestionId = table.Column<int>(type: "int", nullable: false),
                    selectedOption = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attemptAnswers", x => x.AttemptAnswerId);
                });
        }
    }
}
