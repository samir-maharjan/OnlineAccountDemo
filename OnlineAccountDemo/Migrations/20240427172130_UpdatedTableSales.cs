using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineAccountDemo.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTableSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_ModelIssues_IssueId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_IssueId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "ModelIssuesId",
                table: "Sales",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ModelIssuesId",
                table: "Sales",
                column: "ModelIssuesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_ModelIssues_ModelIssuesId",
                table: "Sales",
                column: "ModelIssuesId",
                principalTable: "ModelIssues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_ModelIssues_ModelIssuesId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ModelIssuesId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ModelIssuesId",
                table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "IssueId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_IssueId",
                table: "Sales",
                column: "IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_ModelIssues_IssueId",
                table: "Sales",
                column: "IssueId",
                principalTable: "ModelIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
