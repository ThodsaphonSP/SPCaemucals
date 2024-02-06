using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class feedroledata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2", null, "ApplicationRole", "SSaleRole", "SSALEROLE" },
                    { "3", null, "ApplicationRole", "JSaleRole", "JSALEROLE" },
                    { "4", null, "ApplicationRole", "RSaleRole", "RSALEROLE" },
                    { "5", null, "ApplicationRole", "AccountRole", "ACCOUNTROLE" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3e2213b3-36f1-4e7b-bad6-9a90f0b4e54f", "AQAAAAIAAYagAAAAEGwqxZDd3zGkU0szEEn0dQ9EYzkyO68HOZ6XQ5qWVrxoanm+RXDcJW3WdA4tXU93vA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "69b2b7f5-71a9-4622-84fb-65a8b772ade2", "AQAAAAIAAYagAAAAEAL9yyQh3Rv5j3RNusOQEU7bOgCcmt9DJy8vTxkPIy2WvQgfFf2uiA6zWAMzGUfwJQ==" });
        }
    }
}
