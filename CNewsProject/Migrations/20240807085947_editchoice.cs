using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNewsProject.Migrations
{
    /// <inheritdoc />
    public partial class editchoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EditorsChoiceId",
                table: "Article",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EditorsChoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditorsChoice", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_EditorsChoice_EditorsChoiceId",
                table: "Article");

            migrationBuilder.DropTable(
                name: "EditorsChoice");

            migrationBuilder.DropIndex(
                name: "IX_Article_EditorsChoiceId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "EditorsChoiceId",
                table: "Article");
        }
    }
}
