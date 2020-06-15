using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsApi.Migrations
{
    public partial class IncludeConfigurationandAssetNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Assiento",
                table: "Assets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Folio",
                table: "Assets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tomo",
                table: "Assets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AssetNotes",
                columns: table => new
                {
                    IdNote = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<long>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetNotes", x => x.IdNote);
                    table.ForeignKey(
                        name: "FK_AssetNotes_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                columns: table => new
                {
                    IdConfig = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPrefix = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    InstitutionLogo = table.Column<string>(nullable: true),
                    ParentLogo = table.Column<string>(nullable: true),
                    Tomo = table.Column<int>(nullable: false),
                    Folio = table.Column<int>(nullable: false),
                    Assiento = table.Column<int>(nullable: false),
                    ValidUntil = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.IdConfig);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetNotes_AssetId",
                table: "AssetNotes",
                column: "AssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetNotes");

            migrationBuilder.DropTable(
                name: "Configuration");

            migrationBuilder.DropColumn(
                name: "Assiento",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Folio",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Tomo",
                table: "Assets");
        }
    }
}
