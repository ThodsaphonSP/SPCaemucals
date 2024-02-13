using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class addtitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CustomerType",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TitleId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "13d0fdf0-97fd-44b2-904c-96618437d30b", "AQAAAAIAAYagAAAAEPKlIjcdUBKjZ7VshoXiWwrne9Jb1QTNJ7IWVrvAId8DQSkkR+ykGH9G/mxytACUww==" });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_TitleId",
                table: "Customers",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Titles_TitleId",
                table: "Customers",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Titles_TitleId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.DropIndex(
                name: "IX_Customers_TitleId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerType",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TitleId",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4662ea93-dc10-4952-b797-7a225f02ee5d", "AQAAAAIAAYagAAAAEBtf9nKQgPMI+OG0Dx5DKBL/T0p6uschu7hdzcD/OBkPBxRW7rzjBvSp+Q+o+hjFSw==" });
        }
    }
}
