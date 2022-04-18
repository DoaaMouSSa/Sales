using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class salesinvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblSales",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    total = table.Column<float>(type: "real", nullable: false),
                    sales_Added_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSales", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblSales_tblStore_store_id",
                        column: x => x.store_id,
                        principalTable: "tblStore",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSalesDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    sale_inv_id = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    sales_price_one_product = table.Column<float>(type: "real", nullable: false),
                    total_sales_price_one_product = table.Column<float>(type: "real", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSalesDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblSalesDetails_tblProduct_product_id",
                        column: x => x.product_id,
                        principalTable: "tblProduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSalesDetails_tblSales_sale_inv_id",
                        column: x => x.sale_inv_id,
                        principalTable: "tblSales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblSales_store_id",
                table: "tblSales",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblSalesDetails_product_id",
                table: "tblSalesDetails",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblSalesDetails_sale_inv_id",
                table: "tblSalesDetails",
                column: "sale_inv_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblSalesDetails");

            migrationBuilder.DropTable(
                name: "tblSales");
        }
    }
}
