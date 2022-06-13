using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventCachingDemo.Server.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("090a593a-2ff5-1719-53bc-3446867161cd"), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", "Sleek Frozen Cheese", 396.42m },
                    { new Guid("4511510c-a07e-cbfe-e5ca-227e3b0826da"), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Unbranded Granite Salad", 481.92m },
                    { new Guid("5d158b18-d717-dcf4-63f6-063cc2efe200"), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", "Rustic Frozen Computer", 648.26m },
                    { new Guid("7c94da40-64ba-b208-48a7-7c32790a65f0"), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", "Gorgeous Granite Tuna", 734.08m },
                    { new Guid("e7d28532-79fc-9b88-7864-01ef9b3c4840"), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", "Ergonomic Concrete Table", 251.36m }
                });

            migrationBuilder.InsertData(
                table: "SalesDepartments",
                columns: new[] { "SalesDepartmentId", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1596c81f-9f8c-c63e-8372-68d982f70a04"), "Quibusdam qui animi.", "Grocery" },
                    { new Guid("38c1e55d-2645-bb0e-88c1-12a6b9578ef8"), "Aliquam eos voluptates.", "Home, Baby & Tools" },
                    { new Guid("60c13b7f-ab0f-c002-904a-059efa9e8437"), "Eum quasi blanditiis.", "Automotive" },
                    { new Guid("61a8fd24-6459-8489-0139-8a43e1e2687d"), "Est et qui.", "Sports & Baby" },
                    { new Guid("7b4c1c8e-fd7a-84da-0e83-5a72fe941cbb"), "Debitis nulla repudiandae.", "Electronics, Books & Clothing" }
                });

            migrationBuilder.InsertData(
                table: "SalesAgents",
                columns: new[] { "SalesAgentId", "FirstName", "LastName", "SalesDepartmentId" },
                values: new object[,]
                {
                    { new Guid("027188f1-9cd5-b1bf-8593-69722eb09529"), "Brad", "Haag", new Guid("38c1e55d-2645-bb0e-88c1-12a6b9578ef8") },
                    { new Guid("247815f7-6b0f-1c92-29cc-eaa1374541b4"), "Kelli", "Keebler", new Guid("61a8fd24-6459-8489-0139-8a43e1e2687d") },
                    { new Guid("774eb7ae-4027-730f-293b-e05a96907036"), "Colin", "Koch", new Guid("7b4c1c8e-fd7a-84da-0e83-5a72fe941cbb") },
                    { new Guid("7c97503e-481c-3562-90c9-4ccbf5657806"), "Glenn", "Ernser", new Guid("7b4c1c8e-fd7a-84da-0e83-5a72fe941cbb") },
                    { new Guid("81efbf7a-021b-ce70-d19c-1d6a736074f2"), "Constance", "Williamson", new Guid("60c13b7f-ab0f-c002-904a-059efa9e8437") },
                    { new Guid("8da4c1e8-9c08-3b03-e075-aeabf256595c"), "Reginald", "VonRueden", new Guid("7b4c1c8e-fd7a-84da-0e83-5a72fe941cbb") },
                    { new Guid("93dd4108-57e3-dd43-f4af-67e798adf01a"), "Sheryl", "Denesik", new Guid("60c13b7f-ab0f-c002-904a-059efa9e8437") },
                    { new Guid("c385fcd1-f406-493e-63fb-6514d9110e7a"), "Pamela", "Labadie", new Guid("7b4c1c8e-fd7a-84da-0e83-5a72fe941cbb") },
                    { new Guid("d1688652-f46f-0d23-5d03-f7d7abc0e7d5"), "Mario", "Lindgren", new Guid("60c13b7f-ab0f-c002-904a-059efa9e8437") },
                    { new Guid("f8ff9093-cece-5de1-b5f5-17a797f95df9"), "Randy", "Wehner", new Guid("1596c81f-9f8c-c63e-8372-68d982f70a04") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("090a593a-2ff5-1719-53bc-3446867161cd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("4511510c-a07e-cbfe-e5ca-227e3b0826da"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5d158b18-d717-dcf4-63f6-063cc2efe200"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("7c94da40-64ba-b208-48a7-7c32790a65f0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("e7d28532-79fc-9b88-7864-01ef9b3c4840"));

            migrationBuilder.DeleteData(
                table: "SalesAgents",
                keyColumn: "SalesAgentId",
                keyValue: new Guid("027188f1-9cd5-b1bf-8593-69722eb09529"));

            migrationBuilder.DeleteData(
                table: "SalesAgents",
                keyColumn: "SalesAgentId",
                keyValue: new Guid("247815f7-6b0f-1c92-29cc-eaa1374541b4"));

            migrationBuilder.DeleteData(
                table: "SalesAgents",
                keyColumn: "SalesAgentId",
                keyValue: new Guid("774eb7ae-4027-730f-293b-e05a96907036"));

            migrationBuilder.DeleteData(
                table: "SalesAgents",
                keyColumn: "SalesAgentId",
                keyValue: new Guid("7c97503e-481c-3562-90c9-4ccbf5657806"));

            migrationBuilder.DeleteData(
                table: "SalesAgents",
                keyColumn: "SalesAgentId",
                keyValue: new Guid("81efbf7a-021b-ce70-d19c-1d6a736074f2"));

            migrationBuilder.DeleteData(
                table: "SalesAgents",
                keyColumn: "SalesAgentId",
                keyValue: new Guid("8da4c1e8-9c08-3b03-e075-aeabf256595c"));

            migrationBuilder.DeleteData(
                table: "SalesAgents",
                keyColumn: "SalesAgentId",
                keyValue: new Guid("93dd4108-57e3-dd43-f4af-67e798adf01a"));

            migrationBuilder.DeleteData(
                table: "SalesAgents",
                keyColumn: "SalesAgentId",
                keyValue: new Guid("c385fcd1-f406-493e-63fb-6514d9110e7a"));

            migrationBuilder.DeleteData(
                table: "SalesAgents",
                keyColumn: "SalesAgentId",
                keyValue: new Guid("d1688652-f46f-0d23-5d03-f7d7abc0e7d5"));

            migrationBuilder.DeleteData(
                table: "SalesAgents",
                keyColumn: "SalesAgentId",
                keyValue: new Guid("f8ff9093-cece-5de1-b5f5-17a797f95df9"));

            migrationBuilder.DeleteData(
                table: "SalesDepartments",
                keyColumn: "SalesDepartmentId",
                keyValue: new Guid("1596c81f-9f8c-c63e-8372-68d982f70a04"));

            migrationBuilder.DeleteData(
                table: "SalesDepartments",
                keyColumn: "SalesDepartmentId",
                keyValue: new Guid("38c1e55d-2645-bb0e-88c1-12a6b9578ef8"));

            migrationBuilder.DeleteData(
                table: "SalesDepartments",
                keyColumn: "SalesDepartmentId",
                keyValue: new Guid("60c13b7f-ab0f-c002-904a-059efa9e8437"));

            migrationBuilder.DeleteData(
                table: "SalesDepartments",
                keyColumn: "SalesDepartmentId",
                keyValue: new Guid("61a8fd24-6459-8489-0139-8a43e1e2687d"));

            migrationBuilder.DeleteData(
                table: "SalesDepartments",
                keyColumn: "SalesDepartmentId",
                keyValue: new Guid("7b4c1c8e-fd7a-84da-0e83-5a72fe941cbb"));
        }
    }
}
