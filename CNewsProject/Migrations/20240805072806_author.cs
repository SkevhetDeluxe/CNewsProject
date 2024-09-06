using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNewsProject.Migrations
{
    /// <inheritdoc />
    public partial class author : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Article");

            migrationBuilder.AddColumn<string>(
                name: "AuthorUserName",
                table: "Article",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Article",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorUserName",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Article");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Article",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
