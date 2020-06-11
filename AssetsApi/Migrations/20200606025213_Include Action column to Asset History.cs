using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsApi.Migrations
{
    public partial class IncludeActioncolumntoAssetHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "AssetsHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "AssetsHistory");
        }
    }
}
