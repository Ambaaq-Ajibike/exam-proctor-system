using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exam_proctor_system.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNumberToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("eef9a9b3-2e2a-4558-90e5-d44fe7289d68"));

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfQuestions",
                table: "Exams",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FaceId", "Password", "Role" },
                values: new object[] { new Guid("2c086244-7f75-44b6-ba20-7f439da70d9a"), "ajibikeabulqayyum04@gmail.com", "id", "ambaaq", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2c086244-7f75-44b6-ba20-7f439da70d9a"));

            migrationBuilder.AlterColumn<string>(
                name: "NumberOfQuestions",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FaceId", "Password", "Role" },
                values: new object[] { new Guid("eef9a9b3-2e2a-4558-90e5-d44fe7289d68"), "ajibikeabulqayyum04@gmail.com", "id", "ambaaq", 0 });
        }
    }
}
