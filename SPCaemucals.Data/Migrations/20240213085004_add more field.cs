using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class addmorefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TitleId",
                table: "Customers",
                type: "int",
                nullable: false,
                comment: "คำนำหน้าชื่อ",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AccountNo",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreditDay",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "เครดิต-วัน");

            migrationBuilder.AddColumn<decimal>(
                name: "CreditLimit",
                table: "Customers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Customers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c4275289-0f50-46b4-8978-c729c4cdbc66", "AQAAAAIAAYagAAAAEBqhtIfQEg1iH70desUX0W1bAIzi7WC+JKSOsMWbOsmzlMrTUsRo7G0rPnEfnJkpZA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreditDay",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreditLimit",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "TitleId",
                table: "Customers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "คำนำหน้าชื่อ");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b4977bb8-b9ca-4ff2-9ba7-8d1526544560", "AQAAAAIAAYagAAAAEBCDU1FEkXkTszps3IyAbBkRBumNuif+yb0RxluVbFgH4fEnaaOiOHgXpd1poURWBA==" });
        }
    }
}
