using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsApi.Migrations
{
    public partial class changestoupdateresposiblebylocationandincludeconfigurationproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Responsible1ID",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Responsible2ID",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Configuration",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstituteName",
                table: "Configuration",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Configuration",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Supervisor",
                table: "Configuration",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Responsible1ID",
                table: "Locations",
                column: "Responsible1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Responsible2ID",
                table: "Locations",
                column: "Responsible2ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Persons_Responsible1ID",
                table: "Locations",
                column: "Responsible1ID",
                principalTable: "Persons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Persons_Responsible2ID",
                table: "Locations",
                column: "Responsible2ID",
                principalTable: "Persons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Persons_Responsible1ID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Persons_Responsible2ID",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_Responsible1ID",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_Responsible2ID",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Responsible1ID",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Responsible2ID",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Configuration");

            migrationBuilder.DropColumn(
                name: "InstituteName",
                table: "Configuration");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Configuration");

            migrationBuilder.DropColumn(
                name: "Supervisor",
                table: "Configuration");
        }
    }
}
