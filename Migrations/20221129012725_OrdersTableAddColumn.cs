using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurantapi.Migrations
{
    /// <inheritdoc />
    public partial class OrdersTableAddColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "KitchenAck",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KitchenAck",
                table: "Orders");
        }
    }
}
