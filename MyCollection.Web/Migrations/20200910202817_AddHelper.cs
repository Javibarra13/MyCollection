using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCollection.Web.Migrations
{
    public partial class AddHelper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Bond",
                table: "Sellers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "HelperId",
                table: "Sales",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HelperId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Helpers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Neighborhood = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Bond = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helpers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_HelperId",
                table: "Sales",
                column: "HelperId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_HelperId",
                table: "Orders",
                column: "HelperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Helpers_HelperId",
                table: "Orders",
                column: "HelperId",
                principalTable: "Helpers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Helpers_HelperId",
                table: "Sales",
                column: "HelperId",
                principalTable: "Helpers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Helpers_HelperId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Helpers_HelperId",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "Helpers");

            migrationBuilder.DropIndex(
                name: "IX_Sales_HelperId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Orders_HelperId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Bond",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "HelperId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "HelperId",
                table: "Orders");
        }
    }
}
