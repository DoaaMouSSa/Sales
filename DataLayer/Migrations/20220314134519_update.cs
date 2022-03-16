using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProduct_tblSubCategory_tblSubCategoryid",
                table: "tblProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSubCategory_tblCategory_tblCategoryid",
                table: "tblSubCategory");

            migrationBuilder.DropIndex(
                name: "IX_tblSubCategory_tblCategoryid",
                table: "tblSubCategory");

            migrationBuilder.DropIndex(
                name: "IX_tblProduct_tblSubCategoryid",
                table: "tblProduct");

            migrationBuilder.DropColumn(
                name: "tblCategoryid",
                table: "tblSubCategory");

            migrationBuilder.DropColumn(
                name: "tblSubCategoryid",
                table: "tblProduct");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubCategory_cat_id",
                table: "tblSubCategory",
                column: "cat_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblProduct_sub_cat_id",
                table: "tblProduct",
                column: "sub_cat_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProduct_tblSubCategory_sub_cat_id",
                table: "tblProduct",
                column: "sub_cat_id",
                principalTable: "tblSubCategory",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSubCategory_tblCategory_cat_id",
                table: "tblSubCategory",
                column: "cat_id",
                principalTable: "tblCategory",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProduct_tblSubCategory_sub_cat_id",
                table: "tblProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSubCategory_tblCategory_cat_id",
                table: "tblSubCategory");

            migrationBuilder.DropIndex(
                name: "IX_tblSubCategory_cat_id",
                table: "tblSubCategory");

            migrationBuilder.DropIndex(
                name: "IX_tblProduct_sub_cat_id",
                table: "tblProduct");

            migrationBuilder.AddColumn<int>(
                name: "tblCategoryid",
                table: "tblSubCategory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tblSubCategoryid",
                table: "tblProduct",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSubCategory_tblCategoryid",
                table: "tblSubCategory",
                column: "tblCategoryid");

            migrationBuilder.CreateIndex(
                name: "IX_tblProduct_tblSubCategoryid",
                table: "tblProduct",
                column: "tblSubCategoryid");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProduct_tblSubCategory_tblSubCategoryid",
                table: "tblProduct",
                column: "tblSubCategoryid",
                principalTable: "tblSubCategory",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSubCategory_tblCategory_tblCategoryid",
                table: "tblSubCategory",
                column: "tblCategoryid",
                principalTable: "tblCategory",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
