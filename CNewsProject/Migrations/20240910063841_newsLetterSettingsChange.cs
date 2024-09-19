using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNewsProject.Migrations
{
    /// <inheritdoc />
    public partial class newsLetterSettingsChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_NewsLetterSettings_NewsLetterSettingId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "NewsLetterSettings");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NewsLetterSettingId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NewsLetterSettingId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AuthorNames",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryIds",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<bool>(
                name: "Latest",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Popular",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorNames",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CategoryIds",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Latest",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Popular",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "NewsLetterSettingId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NewsLetterSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latest = table.Column<bool>(type: "bit", nullable: false),
                    Popular = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLetterSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NewsLetterSettingId",
                table: "AspNetUsers",
                column: "NewsLetterSettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_NewsLetterSettings_NewsLetterSettingId",
                table: "AspNetUsers",
                column: "NewsLetterSettingId",
                principalTable: "NewsLetterSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
