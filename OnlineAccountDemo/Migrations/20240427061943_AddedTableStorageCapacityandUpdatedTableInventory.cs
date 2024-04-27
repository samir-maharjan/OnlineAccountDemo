using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineAccountDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedTableStorageCapacityandUpdatedTableInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_JobStatus_StatusId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Inventory");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Inventory",
                newName: "StorageId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_StatusId",
                table: "Inventory",
                newName: "IX_Inventory_StorageId");

            migrationBuilder.AddColumn<int>(
                name: "JobStatusId",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Inventory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "StorageCapacity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StorageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageCapacity", x => x.Id);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_StorageCapacity_StorageId",
                table: "Inventory",
                column: "StorageId",
                principalTable: "StorageCapacity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_JobStatus_JobStatusId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_StorageCapacity_StorageId",
                table: "Inventory");

            migrationBuilder.DropTable(
                name: "StorageCapacity");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_JobStatusId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "JobStatusId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Inventory");

            migrationBuilder.RenameColumn(
                name: "StorageId",
                table: "Inventory",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_StorageId",
                table: "Inventory",
                newName: "IX_Inventory_StatusId");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Inventory",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_JobStatus_StatusId",
                table: "Inventory",
                column: "StatusId",
                principalTable: "JobStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
