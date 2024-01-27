using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineAccountDemo.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPricingTableV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssuePricing_JobStatus_JobStatusId",
                table: "IssuePricing");

            migrationBuilder.DropIndex(
                name: "IX_IssuePricing_JobStatusId",
                table: "IssuePricing");

            migrationBuilder.RenameColumn(
                name: "JobStatusId",
                table: "IssuePricing",
                newName: "IssuesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IssuesId",
                table: "IssuePricing",
                newName: "JobStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_IssuePricing_JobStatusId",
                table: "IssuePricing",
                column: "JobStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssuePricing_JobStatus_JobStatusId",
                table: "IssuePricing",
                column: "JobStatusId",
                principalTable: "JobStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
