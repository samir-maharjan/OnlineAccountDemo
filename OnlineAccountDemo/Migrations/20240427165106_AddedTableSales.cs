using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineAccountDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedTableSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    Colorid = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    StorageId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    IMEINumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_BrandCategory_BrandId",
                        column: x => x.BrandId,
                        principalTable: "BrandCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_ModelColor_Colorid",
                        column: x => x.Colorid,
                        principalTable: "ModelColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_ModelIssues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "ModelIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_StorageCapacity_StorageId",
                        column: x => x.StorageId,
                        principalTable: "StorageCapacity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BrandId",
                table: "Sales",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_Colorid",
                table: "Sales",
                column: "Colorid");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_EmpId",
                table: "Sales",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_IssueId",
                table: "Sales",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_StorageId",
                table: "Sales",
                column: "StorageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");
        }
    }
}
