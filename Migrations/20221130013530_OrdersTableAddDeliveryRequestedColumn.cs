using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurantapi.Migrations
{
    /// <inheritdoc />
    public partial class OrdersTableAddDeliveryRequestedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DeliveryRequested",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryRequested",
                table: "Orders");
        }
    }
}
