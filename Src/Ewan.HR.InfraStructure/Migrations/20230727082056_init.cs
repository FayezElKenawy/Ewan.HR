using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ewan.HR.InfraStructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HR");

            migrationBuilder.CreateTable(
                name: "EmployeeAttendanceLog",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    EmployeeCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Month = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Day = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClockIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClockOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalTime = table.Column<int>(type: "int", nullable: false),
                    AbsentTime = table.Column<int>(type: "int", nullable: false),
                    OverTime = table.Column<int>(type: "int", nullable: false),
                    ChangeTime = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatorNameAr = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatorNameEn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifierNameAr = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifierNameEn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttendanceLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayRollData",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IdType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IdNumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IbanNumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    WorkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonthDays = table.Column<int>(type: "int", nullable: false),
                    DirectAbsent = table.Column<int>(type: "int", nullable: false),
                    AbsentWithPermission = table.Column<int>(type: "int", nullable: false),
                    AbsentWithouPermission = table.Column<int>(type: "int", nullable: false),
                    MedicalAbsent = table.Column<int>(type: "int", nullable: false),
                    DelayWithPermission = table.Column<int>(type: "int", nullable: false),
                    DelayWithoutPermission = table.Column<int>(type: "int", nullable: false),
                    DelayWithoutCutting = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatorNameAr = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreatorNameEn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifierNameAr = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModifierNameEn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayRollData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAttendanceLog",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "PayRollData",
                schema: "HR");
        }
    }
}
