using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventCachingDemo.Server.Migrations
{
    public partial class AddModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "SalesDepartments",
                columns: table => new
                {
                    SalesDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDepartments", x => x.SalesDepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "SalesAgents",
                columns: table => new
                {
                    SalesAgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesAgents", x => x.SalesAgentId);
                    table.ForeignKey(
                        name: "FK_SalesAgents_SalesDepartments_SalesDepartmentId",
                        column: x => x.SalesDepartmentId,
                        principalTable: "SalesDepartments",
                        principalColumn: "SalesDepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Week = table.Column<int>(type: "int", nullable: false),
                    Agent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalProducts = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    MostSoldProduct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesAgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_SalesAgents_SalesAgentId",
                        column: x => x.SalesAgentId,
                        principalTable: "SalesAgents",
                        principalColumn: "SalesAgentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesLogs",
                columns: table => new
                {
                    SalesLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    DayOfSale = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SalesAgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesLogs", x => x.SalesLogId);
                    table.ForeignKey(
                        name: "FK_SalesLogs_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesLogs_SalesAgents_SalesAgentId",
                        column: x => x.SalesAgentId,
                        principalTable: "SalesAgents",
                        principalColumn: "SalesAgentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_SalesAgentId",
                table: "Reports",
                column: "SalesAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Year_Week",
                table: "Reports",
                columns: new[] { "Year", "Week" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesAgents_SalesDepartmentId",
                table: "SalesAgents",
                column: "SalesDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesLogs_ProductId",
                table: "SalesLogs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesLogs_SalesAgentId",
                table: "SalesLogs",
                column: "SalesAgentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "SalesLogs");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SalesAgents");

            migrationBuilder.DropTable(
                name: "SalesDepartments");
        }
    }
}
