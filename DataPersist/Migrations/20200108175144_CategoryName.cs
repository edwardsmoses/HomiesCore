using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DataPersist.Migrations
{
    public partial class CategoryName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Categories_CategoryId",
                table: "Foods");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Foods",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Foods",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Categories_CategoryId",
                table: "Foods",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Categories_CategoryId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Foods");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Foods",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Categories_CategoryId",
                table: "Foods",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
