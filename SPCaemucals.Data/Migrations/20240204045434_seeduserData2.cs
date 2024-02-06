using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class seeduserData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber" },
                values: new object[] { "70e09f2c-fb74-4c53-81df-b6dbfc15e115", "AQAAAAIAAYagAAAAEPX42mSybWPwoX0NXsgA6G/ABYjFoZz5krE0jMb8Z0OTsm+2hVxg2Mh0orxlbyLP3g==", "0918131501" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber" },
                values: new object[] { "b63bb580-ccb7-4a31-97e5-af7442557f79", "AQAAAAIAAYagAAAAECgweVoPFGwHxSaGLWZhyXHNbilnT3UA56h5uULg7St43fImHD6MW+8wrL62PCCq/A==", null });
        }
    }
}
