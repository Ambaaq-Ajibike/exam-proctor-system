using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exam_proctor_system.Migrations
{
    /// <inheritdoc />
    public partial class ExamIdInResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("81a61a50-37a3-4681-b62b-698ce1989ead"));

            migrationBuilder.AddColumn<Guid>(
                name: "ExamId",
                table: "Results",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FaceId", "Password", "Role" },
                values: new object[] { new Guid("6cdc08d2-1a9e-4e6f-96ed-636979de7ca5"), "ajibikeabulqayyum04@gmail.com", "id", "ambaaq", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6cdc08d2-1a9e-4e6f-96ed-636979de7ca5"));

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "Results");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FaceId", "Password", "Role" },
                values: new object[] { new Guid("81a61a50-37a3-4681-b62b-698ce1989ead"), "ajibikeabulqayyum04@gmail.com", "id", "ambaaq", 0 });
        }
    }
}
