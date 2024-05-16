using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Sales.Migrations
{
    /// <inheritdoc />
    public partial class AddCoumnorderquantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stock_TypeId",
                table: "Stock");

            migrationBuilder.AddColumn<int>(
                name: "orderQuantity",
                table: "Type",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stock_TypeId",
                table: "Stock",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stock_TypeId",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "orderQuantity",
                table: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_TypeId",
                table: "Stock",
                column: "TypeId",
                unique: true);
        }
    }
}
