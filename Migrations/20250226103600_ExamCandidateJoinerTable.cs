using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exam_proctor_system.Migrations
{
    /// <inheritdoc />
    public partial class ExamCandidateJoinerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bb617d05-2e9f-40c0-8a54-806e3d531e6c"));

            migrationBuilder.CreateTable(
                name: "CandidateExams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateExams_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateExams_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FaceId", "Password", "Role" },
                values: new object[] { new Guid("81a61a50-37a3-4681-b62b-698ce1989ead"), "ajibikeabulqayyum04@gmail.com", "id", "ambaaq", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateExams_CandidateId",
                table: "CandidateExams",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateExams_ExamId",
                table: "CandidateExams",
                column: "ExamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateExams");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("81a61a50-37a3-4681-b62b-698ce1989ead"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FaceId", "Password", "Role" },
                values: new object[] { new Guid("bb617d05-2e9f-40c0-8a54-806e3d531e6c"), "ajibikeabulqayyum04@gmail.com", "id", "ambaaq", 0 });
        }
    }
}
