using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTesty_Library.Migrations
{
    public partial class migration_20_11_2021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentTestSolutions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LecturerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentAnswersCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTestSolutions", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentTestSolutions");
        }
    }
}
