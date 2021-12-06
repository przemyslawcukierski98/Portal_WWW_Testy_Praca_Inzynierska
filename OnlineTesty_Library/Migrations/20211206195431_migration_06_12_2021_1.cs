using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTesty_Library.Migrations
{
    public partial class migration_06_12_2021_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "StudentsAndGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "StudentsAndGroups",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "StudentsAndGroups");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "StudentsAndGroups");
        }
    }
}
