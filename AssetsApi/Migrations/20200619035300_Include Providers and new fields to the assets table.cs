using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsApi.Migrations
{
    public partial class IncludeProvidersandnewfieldstotheassetstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcquisitionDate",
                table: "Assets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProviderID",
                table: "Assets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ProviderID",
                table: "Assets",
                column: "ProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_AcquisitionMethods_ID",
                table: "AcquisitionMethods",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Providers_ProviderID",
                table: "Assets",
                column: "ProviderID",
                principalTable: "Providers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Providers_ProviderID",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropIndex(
                name: "IX_Assets_ProviderID",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_AcquisitionMethods_ID",
                table: "AcquisitionMethods");

            migrationBuilder.DropColumn(
                name: "AcquisitionDate",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ProviderID",
                table: "Assets");
        }
    }
}
