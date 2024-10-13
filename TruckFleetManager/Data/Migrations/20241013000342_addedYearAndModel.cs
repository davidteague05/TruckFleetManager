using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckFleetManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedYearAndModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Truck",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Truck",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model",
                table: "Truck");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Truck");
        }
    }
}
