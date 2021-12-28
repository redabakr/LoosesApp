using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Looses.Infrastructure.EF.Migrations
{
    public partial class Init_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wells",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wells", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Looses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WellName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LossDate = table.Column<DateTime>(type: "date", nullable: false),
                    DaysOffline = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Looses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Looses_Wells_WellName",
                        column: x => x.WellName,
                        principalTable: "Wells",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Wells",
                columns: new[] { "Name", "Id" },
                values: new object[,]
                {
                    { "R-001", 1 },
                    { "R-002", 2 },
                    { "R-003", 3 },
                    { "R-004", 4 },
                    { "R-005", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Looses_WellName",
                table: "Looses",
                column: "WellName");

            migrationBuilder.CreateIndex(
                name: "IX_Wells_Id",
                table: "Wells",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wells_Name",
                table: "Wells",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Looses");

            migrationBuilder.DropTable(
                name: "Wells");
        }
    }
}
