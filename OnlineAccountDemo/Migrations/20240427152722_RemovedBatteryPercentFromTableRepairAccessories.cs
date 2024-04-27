using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineAccountDemo.Migrations
{
    /// <inheritdoc />
    public partial class RemovedBatteryPercentFromTableRepairAccessories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatteryPercent",
                table: "RepairAccessories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BatteryPercent",
                table: "RepairAccessories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
