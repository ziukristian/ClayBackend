using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClayBackend.Migrations
{
    /// <inheritdoc />
    public partial class remake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessLevel",
                schema: "clay",
                table: "Doors");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "clay",
                table: "Doors");

            migrationBuilder.DropColumn(
                name: "AccessLevel",
                schema: "clay",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AccessLevel",
                schema: "clay",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "clay",
                table: "Doors",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DoorActivityLogs",
                schema: "clay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DoorId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ActionCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoorActivityLogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "clay",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoorActivityLogs_Doors_DoorId",
                        column: x => x.DoorId,
                        principalSchema: "clay",
                        principalTable: "Doors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoorPermissions",
                schema: "clay",
                columns: table => new
                {
                    DoorId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorizedEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorPermissions", x => new { x.DoorId, x.AuthorizedEntityId });
                    table.ForeignKey(
                        name: "FK_DoorPermissions_Doors_DoorId",
                        column: x => x.DoorId,
                        principalSchema: "clay",
                        principalTable: "Doors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoorActivityLogs_DoorId",
                schema: "clay",
                table: "DoorActivityLogs",
                column: "DoorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoorActivityLogs_UserId",
                schema: "clay",
                table: "DoorActivityLogs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoorActivityLogs",
                schema: "clay");

            migrationBuilder.DropTable(
                name: "DoorPermissions",
                schema: "clay");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "clay",
                table: "Doors");

            migrationBuilder.AddColumn<int>(
                name: "AccessLevel",
                schema: "clay",
                table: "Doors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "clay",
                table: "Doors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AccessLevel",
                schema: "clay",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessLevel",
                schema: "clay",
                table: "AspNetRoles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
