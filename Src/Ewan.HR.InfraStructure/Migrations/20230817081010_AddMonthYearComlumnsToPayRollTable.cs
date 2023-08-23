using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ewan.HR.InfraStructure.Migrations
{
    public partial class AddMonthYearComlumnsToPayRollTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Month",
                schema: "HR",
                table: "PayRollData",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                schema: "HR",
                table: "PayRollData",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                schema: "HR",
                table: "PayRollData");

            migrationBuilder.DropColumn(
                name: "Year",
                schema: "HR",
                table: "PayRollData");
        }
    }
}
