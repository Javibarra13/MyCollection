using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCollection.Web.Migrations
{
    public partial class ModifyPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Collectors_CollectorId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Customers_CustomerId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_DayPayments_DayPaymentId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Houses_HouseId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Sellers_SellerId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_TypePayments_TypePaymentId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_CollectorId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_CustomerId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_DayPaymentId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_HouseId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_SellerId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_TypePaymentId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CollectorId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "DayPaymentId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Deposit",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "TypePaymentId",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Purchases",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_WarehouseId",
                table: "Purchases",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Warehouses_WarehouseId",
                table: "Purchases",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Warehouses_WarehouseId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_WarehouseId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "CollectorId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayPaymentId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Deposit",
                table: "Purchases",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Purchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Payment",
                table: "Purchases",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypePaymentId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CollectorId",
                table: "Purchases",
                column: "CollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CustomerId",
                table: "Purchases",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_DayPaymentId",
                table: "Purchases",
                column: "DayPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_HouseId",
                table: "Purchases",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_SellerId",
                table: "Purchases",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_TypePaymentId",
                table: "Purchases",
                column: "TypePaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Collectors_CollectorId",
                table: "Purchases",
                column: "CollectorId",
                principalTable: "Collectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Customers_CustomerId",
                table: "Purchases",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_DayPayments_DayPaymentId",
                table: "Purchases",
                column: "DayPaymentId",
                principalTable: "DayPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Houses_HouseId",
                table: "Purchases",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Sellers_SellerId",
                table: "Purchases",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_TypePayments_TypePaymentId",
                table: "Purchases",
                column: "TypePaymentId",
                principalTable: "TypePayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
