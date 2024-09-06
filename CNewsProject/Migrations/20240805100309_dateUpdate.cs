using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNewsProject.Migrations
{
    /// <inheritdoc />
    public partial class dateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateStamp",
                table: "Article");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Article",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WrittenDate",
                table: "Article",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "WrittenDate",
                table: "Article");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateStamp",
                table: "Article",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
