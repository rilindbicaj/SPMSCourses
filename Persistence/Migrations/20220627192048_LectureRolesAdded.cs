using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class LectureRolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LectureRoleId",
                table: "CoursesAcademicStaff",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LectureRoles",
                columns: table => new
                {
                    LectureRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureRoles", x => x.LectureRoleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesAcademicStaff_LectureRoleId",
                table: "CoursesAcademicStaff",
                column: "LectureRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesAcademicStaff_LectureRole",
                table: "CoursesAcademicStaff",
                column: "LectureRoleId",
                principalTable: "LectureRoles",
                principalColumn: "LectureRoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesAcademicStaff_LectureRole",
                table: "CoursesAcademicStaff");

            migrationBuilder.DropTable(
                name: "LectureRoles");

            migrationBuilder.DropIndex(
                name: "IX_CoursesAcademicStaff_LectureRoleId",
                table: "CoursesAcademicStaff");

            migrationBuilder.DropColumn(
                name: "LectureRoleId",
                table: "CoursesAcademicStaff");
        }
    }
}
