using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDRC.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeExpectedSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "FulfilledSaleModel");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ExpectedSaleModel");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date",
                table: "ExpectedSaleModel",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "ExpectedSaleModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ExpectedSaleModel");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "FulfilledSaleModel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ExpectedSaleModel",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ExpectedSaleModel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
