using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Repository.Data.migrations
{
    public partial class vv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_productCategories_categoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Products_categoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_productCategories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "productCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_productCategories_CategoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "categoryId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Discription");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_productCategories_categoryId",
                table: "Products",
                column: "categoryId",
                principalTable: "productCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
