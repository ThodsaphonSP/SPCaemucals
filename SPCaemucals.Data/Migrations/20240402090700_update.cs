using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPCaemucals.Data.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_AspNetUsers_UserId",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_JobServiceMap_JobService_JobServiceId",
                table: "JobServiceMap");

            migrationBuilder.DropForeignKey(
                name: "FK_JobServiceMap_Job_JobId",
                table: "JobServiceMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobServiceMap",
                table: "JobServiceMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobService",
                table: "JobService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Job",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Job");

            migrationBuilder.RenameTable(
                name: "JobServiceMap",
                newName: "JobServiceMaps");

            migrationBuilder.RenameTable(
                name: "JobService",
                newName: "JobServices");

            migrationBuilder.RenameTable(
                name: "Job",
                newName: "Jobs");

            migrationBuilder.RenameIndex(
                name: "IX_JobServiceMap_JobId",
                table: "JobServiceMaps",
                newName: "IX_JobServiceMaps_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Job_UserId",
                table: "Jobs",
                newName: "IX_Jobs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobServiceMaps",
                table: "JobServiceMaps",
                columns: new[] { "JobServiceId", "JobId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobServices",
                table: "JobServices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_UserId",
                table: "Jobs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobServiceMaps_JobServices_JobServiceId",
                table: "JobServiceMaps",
                column: "JobServiceId",
                principalTable: "JobServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobServiceMaps_Jobs_JobId",
                table: "JobServiceMaps",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_UserId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobServiceMaps_JobServices_JobServiceId",
                table: "JobServiceMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_JobServiceMaps_Jobs_JobId",
                table: "JobServiceMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobServices",
                table: "JobServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobServiceMaps",
                table: "JobServiceMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs");

            migrationBuilder.RenameTable(
                name: "JobServices",
                newName: "JobService");

            migrationBuilder.RenameTable(
                name: "JobServiceMaps",
                newName: "JobServiceMap");

            migrationBuilder.RenameTable(
                name: "Jobs",
                newName: "Job");

            migrationBuilder.RenameIndex(
                name: "IX_JobServiceMaps_JobId",
                table: "JobServiceMap",
                newName: "IX_JobServiceMap_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_UserId",
                table: "Job",
                newName: "IX_Job_UserId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobService",
                table: "JobService",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobServiceMap",
                table: "JobServiceMap",
                columns: new[] { "JobServiceId", "JobId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Job",
                table: "Job",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_AspNetUsers_UserId",
                table: "Job",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobServiceMap_JobService_JobServiceId",
                table: "JobServiceMap",
                column: "JobServiceId",
                principalTable: "JobService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobServiceMap_Job_JobId",
                table: "JobServiceMap",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
