using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    postalCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    latitude = table.Column<string>(type: "text", nullable: true),
                    longitude = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    locationid = table.Column<Guid>(type: "uuid", nullable: false),
                    phoneNumber = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    dateTimeCreatedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    tenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    defaultLanguage = table.Column<string>(type: "text", nullable: false),
                    dateOpenUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateClosedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isPayoutLockedForOtherCostCenter = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Locations_locationid",
                        column: x => x.locationid,
                        principalTable: "Locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    tenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Warehouseid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.id);
                    table.ForeignKey(
                        name: "FK_Items_Warehouses_Warehouseid",
                        column: x => x.Warehouseid,
                        principalTable: "Warehouses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_Warehouseid",
                table: "Items",
                column: "Warehouseid");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_locationid",
                table: "Warehouses",
                column: "locationid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
