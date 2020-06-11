using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsApi.Migrations
{
    public partial class IncludeConfigTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcquisitionMethod",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Assets");

            migrationBuilder.AddColumn<int>(
                name: "AcquisitionMethodID",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurrentPrice",
                table: "Assets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "LocationID",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PurchasePrice",
                table: "Assets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleID",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateID",
                table: "Assets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AcquisitionMethods",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Desription = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcquisitionMethods", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    NationalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.ID);
                    table.UniqueConstraint("AK_Persons_Email", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AcquisitionMethodID",
                table: "Assets",
                column: "AcquisitionMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_LocationID",
                table: "Assets",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ResponsibleID",
                table: "Assets",
                column: "ResponsibleID");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_StateID",
                table: "Assets",
                column: "StateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AcquisitionMethods_AcquisitionMethodID",
                table: "Assets",
                column: "AcquisitionMethodID",
                principalTable: "AcquisitionMethods",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Locations_LocationID",
                table: "Assets",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Persons_ResponsibleID",
                table: "Assets",
                column: "ResponsibleID",
                principalTable: "Persons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_States_StateID",
                table: "Assets",
                column: "StateID",
                principalTable: "States",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AcquisitionMethods_AcquisitionMethodID",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Locations_LocationID",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Persons_ResponsibleID",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_States_StateID",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "AcquisitionMethods");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AcquisitionMethodID",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_LocationID",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_ResponsibleID",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_StateID",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AcquisitionMethodID",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ResponsibleID",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "StateID",
                table: "Assets");

            migrationBuilder.AddColumn<string>(
                name: "AcquisitionMethod",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Assets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
