using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCollection.Web.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    CellPhone = table.Column<string>(maxLength: 20, nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Neighborhood = table.Column<string>(maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    RefName = table.Column<string>(maxLength: 100, nullable: false),
                    RefAddress = table.Column<string>(maxLength: 100, nullable: false),
                    RefPhone = table.Column<string>(maxLength: 100, nullable: false),
                    RefName2 = table.Column<string>(maxLength: 100, nullable: false),
                    RefAddress2 = table.Column<string>(maxLength: 100, nullable: false),
                    RefPhone2 = table.Column<string>(maxLength: 100, nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
