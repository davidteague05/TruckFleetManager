using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckFleetManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedTrucks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Truck",
                columns: table => new
                {
                    TruckId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transmission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastService = table.Column<DateOnly>(type: "date", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TruckTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.TruckId);
                    table.ForeignKey(
                        name: "FK_Truck_Type_TruckTypeId",
                        column: x => x.TruckTypeId,
                        principalTable: "Type",
                        principalColumn: "TruckTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Truck_TruckTypeId",
                table: "Truck",
                column: "TruckTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Truck");
        }
    }
}
