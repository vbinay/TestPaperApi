using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPaperApi.Migrations
{
    public partial class added_subjectgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Option5",
                table: "subjectQuestions");

            migrationBuilder.RenameColumn(
                name: "fk_SubjectId",
                table: "subSubjects",
                newName: "fk_SubjectGroupId");

            migrationBuilder.CreateTable(
                name: "subjectGroup",
                columns: table => new
                {
                    SubjectGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_UserId = table.Column<int>(type: "int", nullable: false),
                    fk_SubjectId = table.Column<int>(type: "int", nullable: false),
                    SubjectGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjectGroup", x => x.SubjectGroupId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subjectGroup");

            migrationBuilder.RenameColumn(
                name: "fk_SubjectGroupId",
                table: "subSubjects",
                newName: "fk_SubjectId");

            migrationBuilder.AddColumn<string>(
                name: "Option5",
                table: "subjectQuestions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
