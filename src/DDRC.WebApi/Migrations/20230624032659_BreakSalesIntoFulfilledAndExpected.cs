using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDRC.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class BreakSalesIntoFulfilledAndExpected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleModel");

            migrationBuilder.CreateTable(
                name: "ExpectedSaleModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VideoStoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpectedSaleModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpectedSaleModel_MovieModel_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpectedSaleModel_VideoStoreModel_VideoStoreId",
                        column: x => x.VideoStoreId,
                        principalTable: "VideoStoreModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FulfilledSaleModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VideoStoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FulfilledSaleModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FulfilledSaleModel_MovieModel_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FulfilledSaleModel_VideoStoreModel_VideoStoreId",
                        column: x => x.VideoStoreId,
                        principalTable: "VideoStoreModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpectedSaleModel_MovieId",
                table: "ExpectedSaleModel",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpectedSaleModel_VideoStoreId",
                table: "ExpectedSaleModel",
                column: "VideoStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_FulfilledSaleModel_MovieId",
                table: "FulfilledSaleModel",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_FulfilledSaleModel_VideoStoreId",
                table: "FulfilledSaleModel",
                column: "VideoStoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpectedSaleModel");

            migrationBuilder.DropTable(
                name: "FulfilledSaleModel");

            migrationBuilder.CreateTable(
                name: "SaleModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoStoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleModel_MovieModel_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleModel_VideoStoreModel_VideoStoreId",
                        column: x => x.VideoStoreId,
                        principalTable: "VideoStoreModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleModel_MovieId",
                table: "SaleModel",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleModel_VideoStoreId",
                table: "SaleModel",
                column: "VideoStoreId");
        }
    }
}
