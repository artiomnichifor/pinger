using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbAccessLayer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(maxLength: 30, nullable: false),
                    ExpectedTime = table.Column<int>(nullable: false),
                    PollingTime = table.Column<int>(nullable: false),
                    LastCheckedTime = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ResponseTime = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    SiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pings_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pings_SiteId",
                table: "Pings",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pings");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
