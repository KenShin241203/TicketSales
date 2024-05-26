using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Sales.Migrations
{
    /// <inheritdoc />
    public partial class AddColumneventName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fullName",
                table: "Order",
                newName: "eventName");

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "eventName",
                table: "Order",
                newName: "fullName");

           
        }
    }
}
