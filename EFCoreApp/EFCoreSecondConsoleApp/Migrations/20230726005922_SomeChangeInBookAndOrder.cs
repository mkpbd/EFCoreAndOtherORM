using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreSecondConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class SomeChangeInBookAndOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookAuthor_BookAuthorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookAuthorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookAuthorId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "BookAuthor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookBookAuthor",
                columns: table => new
                {
                    BooksBookId = table.Column<int>(type: "int", nullable: false),
                    bookAuthorsBookAuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBookAuthor", x => new { x.BooksBookId, x.bookAuthorsBookAuthorId });
                    table.ForeignKey(
                        name: "FK_BookBookAuthor_BookAuthor_bookAuthorsBookAuthorId",
                        column: x => x.bookAuthorsBookAuthorId,
                        principalTable: "BookAuthor",
                        principalColumn: "BookAuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBookAuthor_Books_BooksBookId",
                        column: x => x.BooksBookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "Price",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "Price",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3,
                column: "Price",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4,
                column: "Price",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_AuthorId",
                table: "BookAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBookAuthor_bookAuthorsBookAuthorId",
                table: "BookBookAuthor",
                column: "bookAuthorsBookAuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Author_AuthorId",
                table: "BookAuthor",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Author_AuthorId",
                table: "BookAuthor");

            migrationBuilder.DropTable(
                name: "BookBookAuthor");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthor_AuthorId",
                table: "BookAuthor");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "BookAuthor");

            migrationBuilder.AddColumn<int>(
                name: "BookAuthorId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "BookAuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "BookAuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3,
                column: "BookAuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4,
                column: "BookAuthorId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookAuthorId",
                table: "Books",
                column: "BookAuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookAuthor_BookAuthorId",
                table: "Books",
                column: "BookAuthorId",
                principalTable: "BookAuthor",
                principalColumn: "BookAuthorId");
        }
    }
}
