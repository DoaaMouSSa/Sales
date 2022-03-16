using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class EditTblSubCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "tblSubCategory");

            migrationBuilder.DropColumn(
                name: "is_modified",
                table: "tblSubCategory");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "tblItem");

            migrationBuilder.DropColumn(
                name: "is_modified",
                table: "tblItem");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "tblSubCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_modified",
                table: "tblSubCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "tblItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_modified",
                table: "tblItem",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
