using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPaperApi.Migrations
{
    public partial class updatesubpaper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "fk_userId",
                table: "subSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isComplete",
                table: "subSubjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isVisible",
                table: "subSubjects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fk_userId",
                table: "subSubjects");

            migrationBuilder.DropColumn(
                name: "isComplete",
                table: "subSubjects");

            migrationBuilder.DropColumn(
                name: "isVisible",
                table: "subSubjects");
        }
    }
}
