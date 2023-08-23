using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ewan.HR.InfraStructure.Migrations
{
    public partial class EditMonthSettingsColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "MonthSettings");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "MonthSettings",
                newName: "StartMonth");

            migrationBuilder.RenameColumn(
                name: "MonthEn",
                table: "MonthSettings",
                newName: "MonthName");

            migrationBuilder.RenameColumn(
                name: "MonthAr",
                table: "MonthSettings",
                newName: "EndMonth");

            migrationBuilder.AddColumn<int>(
                name: "EndDay",
                table: "MonthSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthDays",
                table: "MonthSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartDay",
                table: "MonthSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDay",
                table: "MonthSettings");

            migrationBuilder.DropColumn(
                name: "MonthDays",
                table: "MonthSettings");

            migrationBuilder.DropColumn(
                name: "StartDay",
                table: "MonthSettings");

            migrationBuilder.RenameColumn(
                name: "StartMonth",
                table: "MonthSettings",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "MonthName",
                table: "MonthSettings",
                newName: "MonthEn");

            migrationBuilder.RenameColumn(
                name: "EndMonth",
                table: "MonthSettings",
                newName: "MonthAr");

            migrationBuilder.AddColumn<string>(
                name: "End",
                table: "MonthSettings",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);
        }
    }
}
