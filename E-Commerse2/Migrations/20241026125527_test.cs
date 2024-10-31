using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerse2.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_Company_CompanyId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_Company_CompanyId",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Companies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_Companies_CompanyId",
                table: "categories",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Companies_CompanyId",
                table: "products",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_Companies_CompanyId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_Companies_CompanyId",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Company");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_Company_CompanyId",
                table: "categories",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Company_CompanyId",
                table: "products",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }
    }
}
