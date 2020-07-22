using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsApi.Migrations
{
    public partial class Includemoreimagesonassetnotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "AssetNotes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "AssetNotes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image2",
                table: "AssetNotes");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "AssetNotes");
        }
    }
}
