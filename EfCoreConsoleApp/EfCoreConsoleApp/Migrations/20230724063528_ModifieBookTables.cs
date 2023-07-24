using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class ModifieBookTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookAuthors_BookAuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Reviews_ReviewId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_PriceOffers_BookId",
                table: "PriceOffers");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookAuthorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ReviewId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookAuthorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "BookBookAuthor",
                columns: table => new
                {
                    AuthorsLinkBookAuthorId = table.Column<int>(type: "int", nullable: false),
                    BooksBookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBookAuthor", x => new { x.AuthorsLinkBookAuthorId, x.BooksBookId });
                    table.ForeignKey(
                        name: "FK_BookBookAuthor_BookAuthors_AuthorsLinkBookAuthorId",
                        column: x => x.AuthorsLinkBookAuthorId,
                        principalTable: "BookAuthors",
                        principalColumn: "BookAuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBookAuthor_Books_BooksBookId",
                        column: x => x.BooksBookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookReview",
                columns: table => new
                {
                    BooksBookId = table.Column<int>(type: "int", nullable: false),
                    ReviewsReviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReview", x => new { x.BooksBookId, x.ReviewsReviewId });
                    table.ForeignKey(
                        name: "FK_BookReview_Books_BooksBookId",
                        column: x => x.BooksBookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookReview_Reviews_ReviewsReviewId",
                        column: x => x.ReviewsReviewId,
                        principalTable: "Reviews",
                        principalColumn: "ReviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceOffers_BookId",
                table: "PriceOffers",
                column: "BookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookBookAuthor_BooksBookId",
                table: "BookBookAuthor",
                column: "BooksBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReview_ReviewsReviewId",
                table: "BookReview",
                column: "ReviewsReviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookBookAuthor");

            migrationBuilder.DropTable(
                name: "BookReview");

            migrationBuilder.DropIndex(
                name: "IX_PriceOffers_BookId",
                table: "PriceOffers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookAuthorId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceOffers_BookId",
                table: "PriceOffers",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookAuthorId",
                table: "Books",
                column: "BookAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ReviewId",
                table: "Books",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookAuthors_BookAuthorId",
                table: "Books",
                column: "BookAuthorId",
                principalTable: "BookAuthors",
                principalColumn: "BookAuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Reviews_ReviewId",
                table: "Books",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId");
        }
    }
}
