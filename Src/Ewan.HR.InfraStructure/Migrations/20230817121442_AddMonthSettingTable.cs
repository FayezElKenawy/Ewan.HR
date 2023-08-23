using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ewan.HR.InfraStructure.Migrations
{
    public partial class AddMonthSettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthAr = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MonthEn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Start = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    End = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
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
                    table.PrimaryKey("PK_MonthSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthSettings");
        }
    }
}
