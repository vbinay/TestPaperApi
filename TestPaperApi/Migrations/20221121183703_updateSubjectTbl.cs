using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPaperApi.Migrations
{
    public partial class updateSubjectTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "subjects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "fk_UserId",
                table: "subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "fk_UserId",
                table: "subjects");
        }
    }
}
