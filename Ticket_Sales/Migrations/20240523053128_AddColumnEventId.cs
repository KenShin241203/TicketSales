using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Sales.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnEventId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stock_TypeId",
                table: "Stock");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EventID",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stock_TypeId",
                table: "Stock",
                column: "TypeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stock_TypeId",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "EventID",
                table: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_TypeId",
                table: "Stock",
                column: "TypeId");
        }
    }
}
