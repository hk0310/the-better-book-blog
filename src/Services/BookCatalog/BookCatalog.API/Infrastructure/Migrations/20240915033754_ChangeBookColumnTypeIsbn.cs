using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCatalog.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBookColumnTypeIsbn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Book",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Book_Isbn",
                table: "Book",
                column: "Isbn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Book_Isbn",
                table: "Book");

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Book",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
