using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class addinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Customers",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CreditLimit",
                table: "Customers",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2da65d8d-5a9f-4953-b870-86817e2cfab6", "AQAAAAIAAYagAAAAECmW37ycYPyOba+dY0EX8x+DsjlJdmVO1QGD8p+fkt9QCVdVfs1hfrDM5+ENSykADw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Customers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CreditLimit",
                table: "Customers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c4275289-0f50-46b4-8978-c729c4cdbc66", "AQAAAAIAAYagAAAAEBqhtIfQEg1iH70desUX0W1bAIzi7WC+JKSOsMWbOsmzlMrTUsRo7G0rPnEfnJkpZA==" });
        }
    }
}
