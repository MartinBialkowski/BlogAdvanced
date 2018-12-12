using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogPost.Core.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assessments",
                columns: new[] { "Id", "Weight", "WeightType" },
                values: new object[,]
                {
                    { 1, 0.2f, "Homework" },
                    { 2, 0.3f, "Quiz test" },
                    { 3, 0.6f, "Work at school" },
                    { 4, 1f, "Exam" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
