using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CoursesAcademicstaffCompositeKeyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CoursesAcademicStaff_AcademicStaffId",
                table: "CoursesAcademicStaff");

            migrationBuilder.DropIndex(
                name: "IX_CoursesAcademicStaff_CourseId",
                table: "CoursesAcademicStaff");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesAcademicStaff_AcademicStaffId",
                table: "CoursesAcademicStaff",
                column: "AcademicStaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CoursesAcademicStaff_AcademicStaffId",
                table: "CoursesAcademicStaff");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesAcademicStaff_AcademicStaffId",
                table: "CoursesAcademicStaff",
                column: "AcademicStaffId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoursesAcademicStaff_CourseId",
                table: "CoursesAcademicStaff",
                column: "CourseId",
                unique: true);
        }
    }
}
