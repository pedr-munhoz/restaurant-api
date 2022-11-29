using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace restaurantapi.Migrations
{
    /// <inheritdoc />
    public partial class OrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Burgers = table.Column<int>(type: "integer", nullable: false),
                    Fries = table.Column<int>(type: "integer", nullable: false),
                    Sodas = table.Column<int>(type: "integer", nullable: false),
                    BurgersReady = table.Column<bool>(type: "boolean", nullable: false),
                    FriesReady = table.Column<bool>(type: "boolean", nullable: false),
                    SodasReady = table.Column<bool>(type: "boolean", nullable: false),
                    Delivered = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
