using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNewsProject.Migrations
{
    /// <inheritdoc />
    public partial class editFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_EditorsChoice_EditorsChoiceId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_EditorsChoiceId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "EditorsChoiceId",
                table: "Article");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "EditorsChoice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EditorsChoice_ArticleId",
                table: "EditorsChoice",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_EditorsChoice_Article_ArticleId",
                table: "EditorsChoice",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditorsChoice_Article_ArticleId",
                table: "EditorsChoice");

            migrationBuilder.DropIndex(
                name: "IX_EditorsChoice_ArticleId",
                table: "EditorsChoice");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "EditorsChoice");

            migrationBuilder.AddColumn<int>(
                name: "EditorsChoiceId",
                table: "Article",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Article_EditorsChoiceId",
                table: "Article",
                column: "EditorsChoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_EditorsChoice_EditorsChoiceId",
                table: "Article",
                column: "EditorsChoiceId",
                principalTable: "EditorsChoice",
                principalColumn: "Id");
        }
    }
}
