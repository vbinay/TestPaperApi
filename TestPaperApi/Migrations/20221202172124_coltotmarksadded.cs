using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPaperApi.Migrations
{
    public partial class coltotmarksadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TotalMarks",
                table: "subSubjects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMarks",
                table: "subSubjects");
        }
    }
}
