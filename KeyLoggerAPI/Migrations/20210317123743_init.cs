using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KeyLoggerAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Origin = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RegisterLogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LogID = table.Column<int>(type: "int", nullable: false),
                    Register = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegisterLogs_Logs_LogID",
                        column: x => x.LogID,
                        principalTable: "Logs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisterLogs_LogID",
                table: "RegisterLogs",
                column: "LogID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisterLogs");

            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
