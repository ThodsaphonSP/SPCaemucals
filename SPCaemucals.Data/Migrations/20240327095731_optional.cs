using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class optional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VendorTrackingNo",
                table: "Parcels",
                type: "nvarchar(max)",
                nullable: true,
                comment: "หมายเลข tracking จากขนส่ง",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "หมายเลข tracking จากขนส่ง");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VendorTrackingNo",
                table: "Parcels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "หมายเลข tracking จากขนส่ง",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "หมายเลข tracking จากขนส่ง");
        }
    }
}
