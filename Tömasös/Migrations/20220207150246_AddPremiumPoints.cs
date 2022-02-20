using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tömasös.Migrations
{
    public partial class AddPremiumPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PremiumPoints",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PremiumPoints",
                table: "AspNetUsers");
        }
    }
}
