using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineAccountDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployeesAndStatusTableAlsoRemovedRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModelColor_BrandModel_ModelId",
                table: "ModelColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ModelIssues_BrandModel_ModelId",
                table: "ModelIssues");

            migrationBuilder.DropIndex(
                name: "IX_ModelIssues_ModelId",
                table: "ModelIssues");

            migrationBuilder.DropIndex(
                name: "IX_ModelColor_ModelId",
                table: "ModelColor");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "ModelIssues");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "ModelColor");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpDesignation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatus", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "JobStatus");

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "ModelIssues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "ModelColor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ModelIssues_ModelId",
                table: "ModelIssues",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelColor_ModelId",
                table: "ModelColor",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModelColor_BrandModel_ModelId",
                table: "ModelColor",
                column: "ModelId",
                principalTable: "BrandModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModelIssues_BrandModel_ModelId",
                table: "ModelIssues",
                column: "ModelId",
                principalTable: "BrandModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
