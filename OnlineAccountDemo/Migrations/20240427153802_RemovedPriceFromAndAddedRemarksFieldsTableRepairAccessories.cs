using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineAccountDemo.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPriceFromAndAddedRemarksFieldsTableRepairAccessories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "RepairAccessories");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "RepairAccessories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "RepairAccessories");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "RepairAccessories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
