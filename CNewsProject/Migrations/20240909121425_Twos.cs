using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNewsProject.Migrations
{
    /// <inheritdoc />
    public partial class Twos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_NewsLetterSettings_NewsLetterSettingId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "NewsLetterSettingId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 2,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_NewsLetterSettings_NewsLetterSettingId",
                table: "AspNetUsers",
                column: "NewsLetterSettingId",
                principalTable: "NewsLetterSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_NewsLetterSettings_NewsLetterSettingId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "NewsLetterSettingId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_NewsLetterSettings_NewsLetterSettingId",
                table: "AspNetUsers",
                column: "NewsLetterSettingId",
                principalTable: "NewsLetterSettings",
                principalColumn: "Id");
        }
    }
}
