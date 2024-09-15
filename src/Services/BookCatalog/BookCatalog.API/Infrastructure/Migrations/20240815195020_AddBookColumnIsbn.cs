using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCatalog.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookColumnIsbn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Isbn",
                table: "Book",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isbn",
                table: "Book");
        }
    }
}
