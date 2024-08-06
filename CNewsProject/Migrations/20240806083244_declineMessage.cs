using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNewsProject.Migrations
{
    /// <inheritdoc />
    public partial class declineMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PossibleMessageForADeclinedArticleWhichWillBeNullIfItWasNeverDeclinedToBeginWith",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PossibleMessageForADeclinedArticleWhichWillBeNullIfItWasNeverDeclinedToBeginWith",
                table: "Article");
        }
    }
}
