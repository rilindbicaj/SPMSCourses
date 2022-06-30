using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ExamSeasonPKDropped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSeasons_ExamSeasonStatuses",
                table: "ExamSeasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_ExamSeasons_ExamSeasonId",
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

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeasons_StatusId",
                table: "ExamSeasons",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSeasons_ExamSeasonStatuses",
                table: "ExamSeasons",
                column: "StatusId",
                principalTable: "ExamSeasonStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_ExamSeasons",
                table: "Grades",
                column: "ExamSeasonId",
                principalTable: "ExamSeasons",
                principalColumn: "Description",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSeasons_ExamSeasonStatuses",
                table: "ExamSeasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_ExamSeasons",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamSeasons",
                table: "ExamSeasons");

            migrationBuilder.DropIndex(
                name: "IX_ExamSeasons_StatusId",
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
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamSeasons",
                table: "ExamSeasons",
                column: "ExamSeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSeasons_ExamSeasonStatuses",
                table: "ExamSeasons",
                column: "ExamSeasonId",
                principalTable: "ExamSeasonStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_ExamSeasons_ExamSeasonId",
                table: "Grades",
                column: "ExamSeasonId",
                principalTable: "ExamSeasons",
                principalColumn: "ExamSeasonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
