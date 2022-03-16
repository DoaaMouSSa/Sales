using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class EditTblSubCategory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSubCategory_tblCategory_tblCategoryid",
                table: "tblSubCategory");

            migrationBuilder.DropIndex(
                name: "IX_tblSubCategory_tblCategoryid",
                table: "tblSubCategory");

            migrationBuilder.DropColumn(
                name: "tblCategoryid",
                table: "tblSubCategory");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubCategory_cat_id",
                table: "tblSubCategory",
                column: "cat_id");

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
                name: "FK_tblSubCategory_tblCategory_cat_id",
                table: "tblSubCategory");

            migrationBuilder.DropIndex(
                name: "IX_tblSubCategory_cat_id",
                table: "tblSubCategory");

            migrationBuilder.AddColumn<int>(
                name: "tblCategoryid",
                table: "tblSubCategory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSubCategory_tblCategoryid",
                table: "tblSubCategory",
                column: "tblCategoryid");

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
