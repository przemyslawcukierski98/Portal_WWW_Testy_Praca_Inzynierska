using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTesty_Library.Migrations
{
    public partial class migration_11_11_2021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProffesorID",
                table: "Exams",
                newName: "UserEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Exams",
                newName: "ProffesorID");
        }
    }
}
