using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPaperApi.Migrations
{
    public partial class drop_subjectgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subjectGroup");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "subjectGroup",
                columns: table => new
                {
                    SubjectGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubjectGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fk_SubjectId = table.Column<int>(type: "int", nullable: false),
                    fk_UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjectGroup", x => x.SubjectGroupId);
                });
        }
    }
}
