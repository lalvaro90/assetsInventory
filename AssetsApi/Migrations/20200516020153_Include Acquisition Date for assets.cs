using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsApi.Migrations
{
    public partial class IncludeAcquisitionDateforassets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcquisitionMethod",
                table: "Assets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcquisitionMethod",
                table: "Assets");
        }
    }
}
