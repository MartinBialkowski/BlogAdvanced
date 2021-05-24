using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogPost.Core.Migrations
{
    public partial class StudentGradesFloatingProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Mark",
                table: "StudentCourses",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "Assessments",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.UpdateData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Weight",
                value: 0.2);

            migrationBuilder.UpdateData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Weight",
                value: 0.3);

            migrationBuilder.UpdateData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Weight",
                value: 0.6);

            migrationBuilder.UpdateData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 4,
                column: "Weight",
                value: 1.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Mark",
                table: "StudentCourses",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Weight",
                table: "Assessments",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.UpdateData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Weight",
                value: 0.2f);

            migrationBuilder.UpdateData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Weight",
                value: 0.3f);

            migrationBuilder.UpdateData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Weight",
                value: 0.6f);

            migrationBuilder.UpdateData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 4,
                column: "Weight",
                value: 1f);
        }
    }
}
