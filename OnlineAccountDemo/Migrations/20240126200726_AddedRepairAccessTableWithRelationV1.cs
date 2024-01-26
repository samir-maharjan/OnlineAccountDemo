using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineAccountDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedRepairAccessTableWithRelationV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepairAccessories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    Colorid = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    BatteryPercent = table.Column<int>(type: "int", nullable: false),
                    IMEINumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairAccessories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairAccessories_BrandCategory_BrandId",
                        column: x => x.BrandId,
                        principalTable: "BrandCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairAccessories_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairAccessories_JobStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "JobStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairAccessories_ModelColor_Colorid",
                        column: x => x.Colorid,
                        principalTable: "ModelColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairAccessories_ModelIssues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "ModelIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepairAccessories_BrandId",
                table: "RepairAccessories",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairAccessories_Colorid",
                table: "RepairAccessories",
                column: "Colorid");

            migrationBuilder.CreateIndex(
                name: "IX_RepairAccessories_EmpId",
                table: "RepairAccessories",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairAccessories_IssueId",
                table: "RepairAccessories",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairAccessories_StatusId",
                table: "RepairAccessories",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepairAccessories");
        }
    }
}
