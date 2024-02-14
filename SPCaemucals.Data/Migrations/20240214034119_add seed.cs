using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class addseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6", null, "ShippingCoordinatorRole", "SHIPPINGCOORDINATORROLE" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc2552b4-e1d5-4c72-aa00-7ef049393f8d", "AQAAAAIAAYagAAAAECnmNxc6KyRL36TeITAndaSFJHJ9qJ1WvwTuREVTVUcUDoVcAUE45kTn6dltPYPVDw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "845df1f7-ed82-489d-9f00-c5e0ab249bb0", "AQAAAAIAAYagAAAAENdz+/DvoVSwvinwmOLYBaBRm6KEXxxYPWBQsoOFokqh2ooGrqfeODeV97o8ceeFSg==" });
        }
    }
}
