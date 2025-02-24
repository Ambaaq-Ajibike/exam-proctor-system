using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exam_proctor_system.Migrations
{
    /// <inheritdoc />
    public partial class AddedPercentageToResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2c086244-7f75-44b6-ba20-7f439da70d9a"));

            migrationBuilder.AddColumn<double>(
                name: "Percentage",
                table: "Results",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalScore",
                table: "Results",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FaceId", "Password", "Role" },
                values: new object[] { new Guid("bb617d05-2e9f-40c0-8a54-806e3d531e6c"), "ajibikeabulqayyum04@gmail.com", "id", "ambaaq", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bb617d05-2e9f-40c0-8a54-806e3d531e6c"));

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "Results");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FaceId", "Password", "Role" },
                values: new object[] { new Guid("2c086244-7f75-44b6-ba20-7f439da70d9a"), "ajibikeabulqayyum04@gmail.com", "id", "ambaaq", 0 });
        }
    }
}
