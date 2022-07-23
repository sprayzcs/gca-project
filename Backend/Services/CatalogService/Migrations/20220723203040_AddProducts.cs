using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.Migrations
{
    public partial class AddProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("01938276-763b-4104-8763-dfd97654e10a"), "Marcumar", 2539 },
                    { new Guid("063c055d-2284-41d1-82d0-8c3ad77d7546"), "Tamoxifen", 10954 },
                    { new Guid("320cffdb-579f-4e05-934e-699e30d68fd2"), "Vitamin D3", 1239 },
                    { new Guid("615917d5-b26f-4507-ac2b-554af8d888d0"), "Elotrans", 2299 },
                    { new Guid("b11de4ae-e4c9-464c-a494-2a4a5e866a38"), "Calciumfolinat", 53445 },
                    { new Guid("c0383405-68c4-4efb-b5c2-19e970a7b5e0"), "Grippostad C", 990 },
                    { new Guid("dfeff251-ec28-4530-8f81-27260ec7ce34"), "Pantoprazol", 1099 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("01938276-763b-4104-8763-dfd97654e10a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("063c055d-2284-41d1-82d0-8c3ad77d7546"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("320cffdb-579f-4e05-934e-699e30d68fd2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("615917d5-b26f-4507-ac2b-554af8d888d0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b11de4ae-e4c9-464c-a494-2a4a5e866a38"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c0383405-68c4-4efb-b5c2-19e970a7b5e0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dfeff251-ec28-4530-8f81-27260ec7ce34"));
        }
    }
}
