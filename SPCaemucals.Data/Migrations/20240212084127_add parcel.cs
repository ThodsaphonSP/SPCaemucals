using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class addparcel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SaleManId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliveryManId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ParcelStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcels_AspNetUsers_DeliveryManId",
                        column: x => x.DeliveryManId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_AspNetUsers_SaleManId",
                        column: x => x.SaleManId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5e66c9cf-8ac0-4ee6-8580-e7363192cac2", "AQAAAAIAAYagAAAAEA4/aNJxYqlhf7fHxwgUnzNxetXjtnA6FJh3Hd4uTrmJnTXpJtnPQMDvfpC6+S4qDw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_CustomerId",
                table: "Parcels",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_DeliveryManId",
                table: "Parcels",
                column: "DeliveryManId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_SaleManId",
                table: "Parcels",
                column: "SaleManId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Customers_CustomerId",
                table: "Addresses",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Customers_CustomerId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Addresses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dd57fc7a-b253-490e-bfb8-e5b47c622788", "AQAAAAIAAYagAAAAELloUjj3uX7a+OMGA41tu4X5hG1PjGT6Nk3eS1v5JbgS8hdExsiuSCYpyux62hRYeQ==" });
        }
    }
}
