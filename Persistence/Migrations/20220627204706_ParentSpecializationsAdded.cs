using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ParentSpecializationsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentSpecializationId",
                table: "Specializations",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_ParentSpecializationId",
                table: "Specializations",
                column: "ParentSpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_SpecializaitionsChild",
                table: "Specializations",
                column: "ParentSpecializationId",
                principalTable: "Specializations",
                principalColumn: "SpecializationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_SpecializaitionsChild",
                table: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_ParentSpecializationId",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "ParentSpecializationId",
                table: "Specializations");
        }
    }
}
