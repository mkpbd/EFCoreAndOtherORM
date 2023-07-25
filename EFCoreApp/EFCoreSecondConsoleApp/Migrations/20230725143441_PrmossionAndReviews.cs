using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreSecondConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class PrmossionAndReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromssionPrice = table.Column<int>(type: "int", nullable: false),
                    PromotionType = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.PromotionId);
                });

            migrationBuilder.CreateTable(
                name: "BookPromotion",
                columns: table => new
                {
                    BooksBookId = table.Column<int>(type: "int", nullable: false),
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPromotion", x => new { x.BooksBookId, x.PromotionId });
                    table.ForeignKey(
                        name: "FK_BookPromotion_Books_BooksBookId",
                        column: x => x.BooksBookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookPromotion_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookPromotion_PromotionId",
                table: "BookPromotion",
                column: "PromotionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookPromotion");

            migrationBuilder.DropTable(
                name: "Promotion");
        }
    }
}
