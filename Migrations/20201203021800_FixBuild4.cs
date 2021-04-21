using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentRoom.Migrations
{
    public partial class FixBuild4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo2",
                table: "Builds",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo3",
                table: "Builds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo2",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "Photo3",
                table: "Builds");
        }
    }
}
