using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class optionaltitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TitleId",
                table: "Customers",
                type: "int",
                nullable: true,
                comment: "คำนำหน้าชื่อ",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "คำนำหน้าชื่อ");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f47ab5ab-2f6c-42f4-9319-101d90675f1f", "AQAAAAIAAYagAAAAEB7NNBbyLsnFLLohqbrLzFytgr9Lj8BghYkwHYsKOFUoIZvd7LeDC5ajNBc6knOs1A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TitleId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "คำนำหน้าชื่อ",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "คำนำหน้าชื่อ");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ac9b63e9-4171-49ed-8cf8-984af993adea", "AQAAAAIAAYagAAAAEFvBP5gBsAtfJe9h7xCNPjq7vpZEw3ShxbnoXQkrp3luVRNd0kChrR5Q1Fc8d6PH0g==" });
        }
    }
}
