using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class purchasStoreDetailsTbls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblPurchase_tblProduct_product_id",
                table: "tblPurchase");

            migrationBuilder.DropIndex(
                name: "IX_tblPurchase_product_id",
                table: "tblPurchase");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "tblPurchase");

            migrationBuilder.DropColumn(
                name: "product_id",
                table: "tblPurchase");

            migrationBuilder.DropColumn(
                name: "purchase_Added_Time",
                table: "tblPurchase");

            migrationBuilder.DropColumn(
                name: "purchase_price_one_product",
                table: "tblPurchase");

            migrationBuilder.DropColumn(
                name: "qty",
                table: "tblPurchase");

            migrationBuilder.RenameColumn(
                name: "total_purchase_price_one_product",
                table: "tblPurchase",
                newName: "total");

            migrationBuilder.CreateTable(
                name: "tblPurchaseDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    purchase_inv_id = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    purchase_price_one_product = table.Column<float>(type: "real", nullable: false),
                    total_purchase_price_one_product = table.Column<float>(type: "real", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    purchase_Added_Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPurchaseDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblPurchaseDetails_tblProduct_product_id",
                        column: x => x.product_id,
                        principalTable: "tblProduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblPurchaseDetails_tblPurchase_purchase_inv_id",
                        column: x => x.purchase_inv_id,
                        principalTable: "tblPurchase",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblStoreDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    qty = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStoreDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblStoreDetails_tblProduct_product_id",
                        column: x => x.product_id,
                        principalTable: "tblProduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblStoreDetails_tblStore_store_id",
                        column: x => x.store_id,
                        principalTable: "tblStore",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPurchaseDetails_product_id",
                table: "tblPurchaseDetails",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblPurchaseDetails_purchase_inv_id",
                table: "tblPurchaseDetails",
                column: "purchase_inv_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblStoreDetails_product_id",
                table: "tblStoreDetails",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblStoreDetails_store_id",
                table: "tblStoreDetails",
                column: "store_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPurchaseDetails");

            migrationBuilder.DropTable(
                name: "tblStoreDetails");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "tblPurchase",
                newName: "total_purchase_price_one_product");

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "tblPurchase",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "product_id",
                table: "tblPurchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "purchase_Added_Time",
                table: "tblPurchase",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "purchase_price_one_product",
                table: "tblPurchase",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "qty",
                table: "tblPurchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblPurchase_product_id",
                table: "tblPurchase",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblPurchase_tblProduct_product_id",
                table: "tblPurchase",
                column: "product_id",
                principalTable: "tblProduct",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
