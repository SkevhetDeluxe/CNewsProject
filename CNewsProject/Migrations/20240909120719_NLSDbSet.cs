using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNewsProject.Migrations
{
    /// <inheritdoc />
    public partial class NLSDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsLetterSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latest = table.Column<bool>(type: "bit", nullable: false),
                    Popular = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLetterSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsLetterSettings");
        }
    }
}
