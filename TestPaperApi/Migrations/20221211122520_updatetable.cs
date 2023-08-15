using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPaperApi.Migrations
{
    public partial class updatetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "subjectQuestions",
                newName: "fk_SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fk_SubjectId",
                table: "subjectQuestions",
                newName: "SubjectId");
        }
    }
}
