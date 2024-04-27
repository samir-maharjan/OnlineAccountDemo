using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineAccountDemo.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTabkeInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_JobStatus_JobStatusId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_JobStatusId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "JobStatusId",
                table: "Inventory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobStatusId",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_JobStatusId",
                table: "Inventory",
                column: "JobStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_JobStatus_JobStatusId",
                table: "Inventory",
                column: "JobStatusId",
                principalTable: "JobStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
