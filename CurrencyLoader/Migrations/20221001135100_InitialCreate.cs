using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyLoader.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyDetails",
                columns: table => new
                {
                    CurrencyDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    baseCurrency = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyDetails", x => x.CurrencyDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRates",
                columns: table => new
                {
                    CurrencyRatesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyCode = table.Column<string>(nullable: true),
                    Rate = table.Column<double>(nullable: false),
                    CurrencyDetailsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRates", x => x.CurrencyRatesId);
                    table.ForeignKey(
                        name: "FK_CurrencyRates_CurrencyDetails_CurrencyDetailsId",
                        column: x => x.CurrencyDetailsId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_CurrencyDetailsId",
                table: "CurrencyRates",
                column: "CurrencyDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyRates");

            migrationBuilder.DropTable(
                name: "CurrencyDetails");
        }
    }
}
