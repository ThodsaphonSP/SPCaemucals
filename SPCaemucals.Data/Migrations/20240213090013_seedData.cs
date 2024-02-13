using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "acbdda97-8724-4869-8fae-7d8f4f5e9320", "AQAAAAIAAYagAAAAEOvXqDl/yrNwETGLrsBSQX57JBU0fbccE6vWN73JGh+zKCqJreLoCp63D1B2PjkvQQ==" });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "นาย" },
                    { 2, "นางสาว" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Titles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Titles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1fe39c2a-47de-4fd8-9887-0c4be8c9081b", "AQAAAAIAAYagAAAAEC50hk8hiyEpojRr4kx09zAR3I1Zng/fvNwAtOsG2iEfv2NYYPRAGzdXIwhgwySW9w==" });
        }
    }
}
