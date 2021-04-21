using Microsoft.EntityFrameworkCore.Migrations;

namespace RentRoom.Migrations
{
    public partial class addsecondtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Builds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: false),
                    Rooms = table.Column<int>(nullable: false),
                    RoomsToRent = table.Column<int>(nullable: false),
                    FloorRent = table.Column<int>(nullable: false),
                    Floors = table.Column<int>(nullable: false),
                    TotalArea = table.Column<int>(nullable: false),
                    LivingArea = table.Column<int>(nullable: false),
                    KitchenArea = table.Column<int>(nullable: false),
                    Housemates = table.Column<int>(nullable: false),
                    YearBuilt = table.Column<int>(nullable: false),
                    GarbageChute = table.Column<int>(nullable: false),
                    Elevator = table.Column<int>(nullable: false),
                    Furniture = table.Column<int>(nullable: false),
                    HouseholdAppliances = table.Column<int>(nullable: false),
                    Internet = table.Column<int>(nullable: false),
                    Repairs = table.Column<string>(nullable: true),
                    Bathroom = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Builds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Builds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Builds_UserId",
                table: "Builds",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Builds");
        }
    }
}
