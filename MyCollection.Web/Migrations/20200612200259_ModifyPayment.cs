using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCollection.Web.Migrations
{
    public partial class ModifyPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Purchases_PurchaseId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PurchaseId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "Payments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SaleId",
                table: "Payments",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Sales_SaleId",
                table: "Payments",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Sales_SaleId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_SaleId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PurchaseId",
                table: "Payments",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Purchases_PurchaseId",
                table: "Payments",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
