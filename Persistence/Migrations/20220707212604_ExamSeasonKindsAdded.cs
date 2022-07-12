using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ExamSeasonKindsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeasonKindId",
                table: "ExamSeasons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExamSeasonKinds",
                columns: table => new
                {
                    ExamSeasonKindId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamSeasonKindName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSeasonKinds", x => x.ExamSeasonKindId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeasons_SeasonKindId",
                table: "ExamSeasons",
                column: "SeasonKindId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSeasons_ExamSeasonKinds",
                table: "ExamSeasons",
                column: "SeasonKindId",
                principalTable: "ExamSeasonKinds",
                principalColumn: "ExamSeasonKindId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSeasons_ExamSeasonKinds",
                table: "ExamSeasons");

            migrationBuilder.DropTable(
                name: "ExamSeasonKinds");

            migrationBuilder.DropIndex(
                name: "IX_ExamSeasons_SeasonKindId",
                table: "ExamSeasons");

            migrationBuilder.DropColumn(
                name: "SeasonKindId",
                table: "ExamSeasons");
        }
    }
}
