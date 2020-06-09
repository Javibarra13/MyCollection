using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCollection.Web.Migrations
{
    public partial class Sales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "OrderTmps",
                newName: "Username");

            migrationBuilder.CreateTable(
                name: "SaleDetailTmps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetailTmps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleDetailTmps_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Payment = table.Column<double>(nullable: false),
                    Deposit = table.Column<double>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    HouseId = table.Column<int>(nullable: true),
                    CollectorId = table.Column<int>(nullable: true),
                    TypePaymentId = table.Column<int>(nullable: true),
                    DayPaymentId = table.Column<int>(nullable: true),
                    SellerId = table.Column<int>(nullable: true),
                    CustomerId = table.Column<int>(nullable: true),
                    StateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Collectors_CollectorId",
                        column: x => x.CollectorId,
                        principalTable: "Collectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_DayPayments_DayPaymentId",
                        column: x => x.DayPaymentId,
                        principalTable: "DayPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_TypePayments_TypePaymentId",
                        column: x => x.TypePaymentId,
                        principalTable: "TypePayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleTmps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 256, nullable: false),
                    CustomerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleTmps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleTmps_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    SaleId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleDetails_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_ProductId",
                table: "SaleDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_SaleId",
                table: "SaleDetails",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetailTmps_ProductId",
                table: "SaleDetailTmps",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CollectorId",
                table: "Sales",
                column: "CollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DayPaymentId",
                table: "Sales",
                column: "DayPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_HouseId",
                table: "Sales",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SellerId",
                table: "Sales",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_StateId",
                table: "Sales",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_TypePaymentId",
                table: "Sales",
                column: "TypePaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleTmps_CustomerId",
                table: "SaleTmps",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleDetails");

            migrationBuilder.DropTable(
                name: "SaleDetailTmps");

            migrationBuilder.DropTable(
                name: "SaleTmps");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "OrderTmps",
                newName: "UserName");
        }
    }
}
