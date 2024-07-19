using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClayBackend.Migrations
{
    /// <inheritdoc />
    public partial class changedindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_DoorId_TimeStamp",
                schema: "clay",
                table: "ActivityLogs");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_DoorId_TimeStamp",
                schema: "clay",
                table: "ActivityLogs",
                columns: new[] { "DoorId", "TimeStamp" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_DoorId_TimeStamp",
                schema: "clay",
                table: "ActivityLogs");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_DoorId_TimeStamp",
                schema: "clay",
                table: "ActivityLogs",
                columns: new[] { "DoorId", "TimeStamp" },
                descending: new[] { false, true });
        }
    }
}
