using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCollection.Web.Migrations
{
    public partial class modifyOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Sales",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_WarehouseId",
                table: "Sales",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WarehouseId",
                table: "Orders",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Warehouses_WarehouseId",
                table: "Orders",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Warehouses_WarehouseId",
                table: "Sales",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_WarehouseId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Warehouses_WarehouseId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_WarehouseId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WarehouseId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Orders");
        }
    }
}
