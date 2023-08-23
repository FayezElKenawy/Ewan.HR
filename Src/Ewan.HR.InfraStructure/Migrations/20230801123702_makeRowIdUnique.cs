using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ewan.HR.InfraStructure.Migrations
{
    public partial class makeRowIdUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
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
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeData",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IDType = table.Column<int>(type: "int", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    FristName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DepartementId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DirectManager = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LocationId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ResumptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_EmployeeData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeData_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendanceLog_RowId",
                schema: "HR",
                table: "EmployeeAttendanceLog",
                column: "RowId",
                unique: true,
                filter: "[RowId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeData_DepartmentId",
                schema: "HR",
                table: "EmployeeData",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeData",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAttendanceLog_RowId",
                schema: "HR",
                table: "EmployeeAttendanceLog");
        }
    }
}
