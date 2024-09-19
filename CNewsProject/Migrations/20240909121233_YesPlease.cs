using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNewsProject.Migrations
{
    /// <inheritdoc />
    public partial class YesPlease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewsLetterSettingId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NewsLetterSettingId",
                table: "AspNetUsers",
                column: "NewsLetterSettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_NewsLetterSettings_NewsLetterSettingId",
                table: "AspNetUsers",
                column: "NewsLetterSettingId",
                principalTable: "NewsLetterSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_NewsLetterSettings_NewsLetterSettingId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NewsLetterSettingId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NewsLetterSettingId",
                table: "AspNetUsers");
        }
    }
}
