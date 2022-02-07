using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addmigrationcities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentCityId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReturnCityId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReturnedCityId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalSum = table.Column<double>(type: "float", nullable: false),
                    RentalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentCityId",
                table: "Rentals",
                column: "RentCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_ReturnCityId",
                table: "Rentals",
                column: "ReturnCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_ReturnedCityId",
                table: "Rentals",
                column: "ReturnedCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RentalId",
                table: "Payments",
                column: "RentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Cities_RentCityId",
                table: "Rentals",
                column: "RentCityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Cities_ReturnCityId",
                table: "Rentals",
                column: "ReturnCityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Cities_ReturnedCityId",
                table: "Rentals",
                column: "ReturnedCityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Cities_RentCityId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Cities_ReturnCityId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Cities_ReturnedCityId",
                table: "Rentals");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_RentCityId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_ReturnCityId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_ReturnedCityId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentCityId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ReturnCityId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ReturnedCityId",
                table: "Rentals");
        }
    }
}
