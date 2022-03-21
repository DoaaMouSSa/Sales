using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class purchaseTbldt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "purchase_Added_Time",
                table: "tblPurchaseDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "purchase_Added_Time",
                table: "tblPurchase",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "purchase_Added_Time",
                table: "tblPurchase");

            migrationBuilder.AddColumn<DateTime>(
                name: "purchase_Added_Time",
                table: "tblPurchaseDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
