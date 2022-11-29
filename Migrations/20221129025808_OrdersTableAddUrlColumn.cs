using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurantapi.Migrations
{
    /// <inheritdoc />
    public partial class OrdersTableAddUrlColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryLocation",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryResponse",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryLocation",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryResponse",
                table: "Orders");
        }
    }
}
