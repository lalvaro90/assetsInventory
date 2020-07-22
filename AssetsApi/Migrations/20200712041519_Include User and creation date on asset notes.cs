using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsApi.Migrations
{
    public partial class IncludeUserandcreationdateonassetnotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "AssetNotes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "AssetNotes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AssetNotes_CreatedById",
                table: "AssetNotes",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetNotes_Users_CreatedById",
                table: "AssetNotes",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetNotes_Users_CreatedById",
                table: "AssetNotes");

            migrationBuilder.DropIndex(
                name: "IX_AssetNotes_CreatedById",
                table: "AssetNotes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "AssetNotes");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "AssetNotes");
        }
    }
}
