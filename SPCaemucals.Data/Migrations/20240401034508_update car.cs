using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatecar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_AspNetUsers_DeliveryManId",
                table: "Parcels");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_DeliveryManId",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "DeliveryManId",
                table: "Parcels");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Parcels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShippingCoordinatorId",
                table: "Parcels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryManId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_AspNetUsers_DeliveryManId",
                        column: x => x.DeliveryManId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_CarId",
                table: "Parcels",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_ShippingCoordinatorId",
                table: "Parcels",
                column: "ShippingCoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DeliveryManId",
                table: "Cars",
                column: "DeliveryManId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_AspNetUsers_ShippingCoordinatorId",
                table: "Parcels",
                column: "ShippingCoordinatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Cars_CarId",
                table: "Parcels",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_AspNetUsers_ShippingCoordinatorId",
                table: "Parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Cars_CarId",
                table: "Parcels");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_CarId",
                table: "Parcels");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_ShippingCoordinatorId",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "ShippingCoordinatorId",
                table: "Parcels");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryManId",
                table: "Parcels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_DeliveryManId",
                table: "Parcels",
                column: "DeliveryManId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_AspNetUsers_DeliveryManId",
                table: "Parcels",
                column: "DeliveryManId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
