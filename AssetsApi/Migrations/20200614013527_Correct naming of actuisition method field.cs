using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsApi.Migrations
{
    public partial class Correctnamingofactuisitionmethodfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desription",
                table: "AcquisitionMethods");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AcquisitionMethods",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AcquisitionMethods");

            migrationBuilder.AddColumn<string>(
                name: "Desription",
                table: "AcquisitionMethods",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
