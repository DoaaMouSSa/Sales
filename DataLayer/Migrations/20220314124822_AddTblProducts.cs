using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddTblProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblItem");

            migrationBuilder.CreateTable(
                name: "tblProduct",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    barcode = table.Column<int>(type: "int", nullable: false),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    purchase_price = table.Column<float>(type: "real", nullable: false),
                    sale_price = table.Column<float>(type: "real", nullable: false),
                    sub_cat_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblProduct_tblSubCategory_sub_cat_id",
                        column: x => x.sub_cat_id,
                        principalTable: "tblSubCategory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProduct_sub_cat_id",
                table: "tblProduct",
                column: "sub_cat_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblProduct");

            migrationBuilder.CreateTable(
                name: "tblItem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bar_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    profit = table.Column<float>(type: "real", nullable: false),
                    purchase_price = table.Column<float>(type: "real", nullable: false),
                    sales_price = table.Column<float>(type: "real", nullable: false),
                    subcat_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItem", x => x.id);
                });
        }
    }
}
