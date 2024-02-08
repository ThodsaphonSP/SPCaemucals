using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class revokedByIdCanNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RevokedByIp",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f9dcf45f-954b-40f9-ac7f-8e5e5a7d383f", "AQAAAAIAAYagAAAAEC1dZjGNJNZwgf9D2Mk3k0gxYifC7BWXYa7Thi/RkQHAWNId1Hzg1l1qLD1ib1JHHw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RevokedByIp",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "da6c0438-b678-4d08-9d1f-97a04b8cf861", "AQAAAAIAAYagAAAAEBiXmem8VaUAcEtMfDZUvR+mTCum4SFHK6RWIRZJOyNUJv1EOtWVM+AOlqm3HhmMvg==" });
        }
    }
}
