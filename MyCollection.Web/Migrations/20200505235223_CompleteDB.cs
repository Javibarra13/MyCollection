using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCollection.Web.Migrations
{
    public partial class CompleteDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<string>(maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    FixedPhone = table.Column<string>(maxLength: 20, nullable: true),
                    CellPhone = table.Column<string>(maxLength: 20, nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerImages_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    Neighborhood = table.Column<string>(maxLength: 50, nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    Contact = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<string>(maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    FixedPhone = table.Column<string>(maxLength: 20, nullable: true),
                    CellPhone = table.Column<string>(maxLength: 20, nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seller",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<string>(maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    FixedPhone = table.Column<string>(maxLength: 20, nullable: true),
                    CellPhone = table.Column<string>(maxLength: 20, nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supervisors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<string>(maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    FixedPhone = table.Column<string>(maxLength: 20, nullable: true),
                    CellPhone = table.Column<string>(maxLength: 20, nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyCollectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serie = table.Column<string>(maxLength: 50, nullable: false),
                    Company = table.Column<string>(maxLength: 50, nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    Colour = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    PropertyTypeId = table.Column<int>(nullable: true),
                    CollectorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyCollectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyCollectors_Collectors_CollectorId",
                        column: x => x.CollectorId,
                        principalTable: "Collectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyCollectors_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyManagers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serie = table.Column<string>(maxLength: 50, nullable: false),
                    Company = table.Column<string>(maxLength: 50, nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    Colour = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    PropertyTypeId = table.Column<int>(nullable: true),
                    ManagerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyManagers_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyManagers_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertySellers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serie = table.Column<string>(maxLength: 50, nullable: false),
                    Company = table.Column<string>(maxLength: 50, nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    Colour = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    PropertyTypeId = table.Column<int>(nullable: true),
                    SellerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySellers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertySellers_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertySellers_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertySupervisors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serie = table.Column<string>(maxLength: 50, nullable: false),
                    Company = table.Column<string>(maxLength: 50, nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    Colour = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    PropertyTypeId = table.Column<int>(nullable: true),
                    SupervisorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySupervisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertySupervisors_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertySupervisors_Supervisors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyCollectorImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(nullable: false),
                    PropertyCollectorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyCollectorImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyCollectorImages_PropertyCollectors_PropertyCollectorId",
                        column: x => x.PropertyCollectorId,
                        principalTable: "PropertyCollectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyManagerImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(nullable: false),
                    PropertyManagerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyManagerImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyManagerImages_PropertyManagers_PropertyManagerId",
                        column: x => x.PropertyManagerId,
                        principalTable: "PropertyManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertySellerImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(nullable: false),
                    PropertySellerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySellerImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertySellerImages_PropertySellers_PropertySellerId",
                        column: x => x.PropertySellerId,
                        principalTable: "PropertySellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertySupervisorImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(nullable: false),
                    PropertySupervisorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySupervisorImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertySupervisorImages_PropertySupervisors_PropertySupervisorId",
                        column: x => x.PropertySupervisorId,
                        principalTable: "PropertySupervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerImages_CustomerId",
                table: "CustomerImages",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyCollectorImages_PropertyCollectorId",
                table: "PropertyCollectorImages",
                column: "PropertyCollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyCollectors_CollectorId",
                table: "PropertyCollectors",
                column: "CollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyCollectors_PropertyTypeId",
                table: "PropertyCollectors",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyManagerImages_PropertyManagerId",
                table: "PropertyManagerImages",
                column: "PropertyManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyManagers_ManagerId",
                table: "PropertyManagers",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyManagers_PropertyTypeId",
                table: "PropertyManagers",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySellerImages_PropertySellerId",
                table: "PropertySellerImages",
                column: "PropertySellerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySellers_PropertyTypeId",
                table: "PropertySellers",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySellers_SellerId",
                table: "PropertySellers",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySupervisorImages_PropertySupervisorId",
                table: "PropertySupervisorImages",
                column: "PropertySupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySupervisors_PropertyTypeId",
                table: "PropertySupervisors",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySupervisors_SupervisorId",
                table: "PropertySupervisors",
                column: "SupervisorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerImages");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "PropertyCollectorImages");

            migrationBuilder.DropTable(
                name: "PropertyManagerImages");

            migrationBuilder.DropTable(
                name: "PropertySellerImages");

            migrationBuilder.DropTable(
                name: "PropertySupervisorImages");

            migrationBuilder.DropTable(
                name: "PropertyCollectors");

            migrationBuilder.DropTable(
                name: "PropertyManagers");

            migrationBuilder.DropTable(
                name: "PropertySellers");

            migrationBuilder.DropTable(
                name: "PropertySupervisors");

            migrationBuilder.DropTable(
                name: "Collectors");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Seller");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "Supervisors");
        }
    }
}
