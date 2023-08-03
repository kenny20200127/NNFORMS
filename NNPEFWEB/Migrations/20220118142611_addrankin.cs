﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addrankin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropIndex(
            //    name: "IX_AspNetUserRoles_RoleId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropIndex(
            //    name: "IX_AspNetUserRoles_UserId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropColumn(
            //    name: "RoleId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropColumn(
            //    name: "UserId1",
            //    table: "AspNetUserRoles");

            migrationBuilder.AddColumn<int>(
                name: "rankId",
                table: "ef_personalInfos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rankId",
                table: "ef_personalInfos");

            //migrationBuilder.AddColumn<int>(
            //    name: "RoleId1",
            //    table: "AspNetUserRoles",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "UserId1",
            //    table: "AspNetUserRoles",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_RoleId1",
            //    table: "AspNetUserRoles",
            //    column: "RoleId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_UserId1",
            //    table: "AspNetUserRoles",
            //    column: "UserId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
            //    table: "AspNetUserRoles",
            //    column: "RoleId1",
            //    principalTable: "AspNetRoles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
            //    table: "AspNetUserRoles",
            //    column: "UserId1",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
