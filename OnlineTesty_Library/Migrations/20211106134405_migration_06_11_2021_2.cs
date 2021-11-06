using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTesty_Library.Migrations
{
    public partial class migration_06_11_2021_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_StudentGroups_StudentGroupNameID",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_StudentGroupNameID",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "StudentGroupNameID",
                table: "Exams");

            migrationBuilder.AddColumn<string>(
                name: "StudentGroupName",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentGroupName",
                table: "Exams");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentGroupNameID",
                table: "Exams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_StudentGroupNameID",
                table: "Exams",
                column: "StudentGroupNameID");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_StudentGroups_StudentGroupNameID",
                table: "Exams",
                column: "StudentGroupNameID",
                principalTable: "StudentGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
