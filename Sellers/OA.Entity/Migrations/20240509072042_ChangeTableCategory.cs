using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sellers.Entity.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_AccountId1",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AccountId1",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Categories",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AccountId",
                table: "Categories",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_AccountId",
                table: "Categories",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_AccountId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AccountId",
                table: "Categories");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Categories",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AccountId1",
                table: "Categories",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AccountId1",
                table: "Categories",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_AccountId1",
                table: "Categories",
                column: "AccountId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
