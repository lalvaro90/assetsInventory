using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsApi.Migrations
{
    public partial class IncludeSecondResponsibleforassets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Responsible2ID",
                table: "Assets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_Responsible2ID",
                table: "Assets",
                column: "Responsible2ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Persons_Responsible2ID",
                table: "Assets",
                column: "Responsible2ID",
                principalTable: "Persons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Persons_Responsible2ID",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_Responsible2ID",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Responsible2ID",
                table: "Assets");
        }
    }
}
