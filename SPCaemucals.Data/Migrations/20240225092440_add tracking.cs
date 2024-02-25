using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class addtracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VendorTrackingNo",
                table: "Parcels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "หมายเลข tracking จากขนส่ง");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b16c7c34-f6ad-4680-8b72-f6aa0667a28b", "AQAAAAIAAYagAAAAEDMDcIg5uX3PSO2BDE/9BoSE6IIA2I7w3888IpuvNIPNvhecOEj769TITTXdYTYimA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VendorTrackingNo",
                table: "Parcels");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2b69efcf-abcc-4f30-b78f-f7890f416d1f", "AQAAAAIAAYagAAAAEA5x2wwv9qMjXBjeBANyU6yDnMTHQnVMAeeYOm88RWjq6Dd+yfELIZpTlFhnqCg6dQ==" });
        }
    }
}
