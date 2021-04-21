using Microsoft.EntityFrameworkCore.Migrations;

namespace RentRoom.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "varchar(250)", nullable: false),
                    Password = table.Column<string>(type: "varchar(250)", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(250)", nullable: true),
                    Admin = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
