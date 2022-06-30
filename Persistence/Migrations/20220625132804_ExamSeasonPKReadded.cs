using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ExamSeasonPKReadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_ExamSeasons",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamSeasons",
                table: "ExamSeasons");

            migrationBuilder.AlterColumn<int>(
                name: "ExamSeasonId",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ExamSeasons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ExamSeasonId",
                table: "ExamSeasons",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamSeasons",
                table: "ExamSeasons",
                column: "ExamSeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_ExamSeasons",
                table: "Grades",
                column: "ExamSeasonId",
                principalTable: "ExamSeasons",
                principalColumn: "ExamSeasonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_ExamSeasons",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamSeasons",
                table: "ExamSeasons");

            migrationBuilder.DropColumn(
                name: "ExamSeasonId",
                table: "ExamSeasons");

            migrationBuilder.AlterColumn<string>(
                name: "ExamSeasonId",
                table: "Grades",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ExamSeasons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamSeasons",
                table: "ExamSeasons",
                column: "Description");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_ExamSeasons",
                table: "Grades",
                column: "ExamSeasonId",
                principalTable: "ExamSeasons",
                principalColumn: "Description",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
