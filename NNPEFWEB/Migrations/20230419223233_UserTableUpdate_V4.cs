using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class UserTableUpdate_V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "personnel",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "personnel",
                table: "AspNetUsers");
        }
    }
}
