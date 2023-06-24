using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDRC.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class StocksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockModel_MovieModel_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockModel_MovieId",
                table: "StockModel",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockModel");
        }
    }
}
