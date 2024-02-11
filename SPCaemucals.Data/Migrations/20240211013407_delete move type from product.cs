using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class deletemovetypefromproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoveType",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2301bc20-39d6-4ba6-b8b6-18bc844f534a", "AQAAAAIAAYagAAAAEInACwQ/Q3nfLhTCfMNSBWOoaqFDT7bWrVm5gt49g5FwI3Ep6giodabJ9GkoACJ+lg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MoveType",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3027e88a-d2f8-4f22-93ce-aecfb0a01593", "AQAAAAIAAYagAAAAENvmfaHCCFgTTni1+sSAMd5UEAXsw1YGF5MZADJPkOzsFFrrkWJZrDt/cirHSTUVGA==" });
        }
    }
}
