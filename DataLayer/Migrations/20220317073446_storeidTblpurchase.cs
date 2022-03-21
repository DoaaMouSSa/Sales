using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class storeidTblpurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "store_id",
                table: "tblPurchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblPurchase_store_id",
                table: "tblPurchase",
                column: "store_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblPurchase_tblStore_store_id",
                table: "tblPurchase",
                column: "store_id",
                principalTable: "tblStore",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblPurchase_tblStore_store_id",
                table: "tblPurchase");

            migrationBuilder.DropIndex(
                name: "IX_tblPurchase_store_id",
                table: "tblPurchase");

            migrationBuilder.DropColumn(
                name: "store_id",
                table: "tblPurchase");
        }
    }
}
