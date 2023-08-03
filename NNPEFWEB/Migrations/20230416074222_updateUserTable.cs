//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//namespace NNPEFWEB.Migrations
//{
//    public partial class updateUserTable : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.AddColumn<string>(
//                name: "command",
//                table: "AspNetUsers",
//                nullable: true);

//            migrationBuilder.AddColumn<DateTime>(
//                name: "expireDate",
//                table: "AspNetUsers",
//                nullable: false,
//                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

//            migrationBuilder.AddColumn<string>(
//                name: "ship",
//                table: "AspNetUsers",
//                nullable: true);
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropColumn(
//                name: "command",
//                table: "AspNetUsers");

//            migrationBuilder.DropColumn(
//                name: "expireDate",
//                table: "AspNetUsers");

//            migrationBuilder.DropColumn(
//                name: "ship",
//                table: "AspNetUsers");
//        }
//    }
//}
