using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckoutService.Migrations
{
    public partial class ChangeCreditCardExpiryDateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PaymentInfo_CreditCardExpiryDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "PaymentInfo_CreditCardExpiryDate",
                table: "Orders",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");
        }
    }
}
