using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDRC.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class VideoStoreAndSalesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoStoreModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoStoreModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleModel",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleModel");

            migrationBuilder.DropTable(
                name: "VideoStoreModel");
        }
    }
}
