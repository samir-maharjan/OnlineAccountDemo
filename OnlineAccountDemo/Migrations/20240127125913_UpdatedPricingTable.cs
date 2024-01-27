using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineAccountDemo.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPricingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueCode",
                table: "IssuePricing");

            migrationBuilder.RenameColumn(
                name: "IssueTitle",
                table: "IssuePricing",
                newName: "IssuePrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IssuePrice",
                table: "IssuePricing",
                newName: "IssueTitle");

            migrationBuilder.AddColumn<string>(
                name: "IssueCode",
                table: "IssuePricing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
