using Microsoft.EntityFrameworkCore.Migrations;

namespace RentRoom.Migrations
{
    public partial class FixBuildModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Builds",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Builds");
        }
    }
}
