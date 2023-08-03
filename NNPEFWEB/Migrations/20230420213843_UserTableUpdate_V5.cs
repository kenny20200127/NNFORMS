using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class UserTableUpdate_V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "shipcheck",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shipcheck",
                table: "AspNetUsers");
        }
    }
}
