using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaShopDatabaseImplement.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingridients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngridientName = table.Column<string>(nullable: false),
                    IngridientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingridients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingridients_Ingridients_IngridientId",
                        column: x => x.IngridientId,
                        principalTable: "Ingridients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pizzas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaName = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizzas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StorageName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Sum = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateImplement = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PizzaIngridients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaId = table.Column<int>(nullable: false),
                    IngridientId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaIngridients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzaIngridients_Ingridients_IngridientId",
                        column: x => x.IngridientId,
                        principalTable: "Ingridients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaIngridients_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorageIngridients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StorageId = table.Column<int>(nullable: false),
                    IngridientId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageIngridients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageIngridients_Ingridients_IngridientId",
                        column: x => x.IngridientId,
                        principalTable: "Ingridients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageIngridients_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingridients_IngridientId",
                table: "Ingridients",
                column: "IngridientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PizzaId",
                table: "Orders",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaIngridients_IngridientId",
                table: "PizzaIngridients",
                column: "IngridientId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaIngridients_PizzaId",
                table: "PizzaIngridients",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageIngridients_IngridientId",
                table: "StorageIngridients",
                column: "IngridientId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageIngridients_StorageId",
                table: "StorageIngridients",
                column: "StorageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PizzaIngridients");

            migrationBuilder.DropTable(
                name: "StorageIngridients");

            migrationBuilder.DropTable(
                name: "Pizzas");

            migrationBuilder.DropTable(
                name: "Ingridients");

            migrationBuilder.DropTable(
                name: "Storages");
        }
    }
}
