using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addcanCheckPensionDeathGratuity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanCheckPensionDeathGrantuity",
                table: "AspNetUsers",
                nullable: true, defaultValue:false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanCheckPensionDeathGrantuity",
                table: "AspNetUsers");
        }
    }
}
