using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClayBackend.Migrations
{
    /// <inheritdoc />
    public partial class upd7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUser",
                schema: "clay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupMemberships",
                schema: "clay",
                table: "GroupMemberships");

            migrationBuilder.DropIndex(
                name: "IX_GroupMemberships_UserId",
                schema: "clay",
                table: "GroupMemberships");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupMemberships",
                schema: "clay",
                table: "GroupMemberships",
                columns: new[] { "UserId", "GroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupMemberships_GroupId",
                schema: "clay",
                table: "GroupMemberships",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupMemberships",
                schema: "clay",
                table: "GroupMemberships");

            migrationBuilder.DropIndex(
                name: "IX_GroupMemberships_GroupId",
                schema: "clay",
                table: "GroupMemberships");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupMemberships",
                schema: "clay",
                table: "GroupMemberships",
                columns: new[] { "GroupId", "UserId" });

            migrationBuilder.CreateTable(
                name: "GroupUser",
                schema: "clay",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.GroupsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_GroupUser_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalSchema: "clay",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalSchema: "clay",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupMemberships_UserId",
                schema: "clay",
                table: "GroupMemberships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_MembersId",
                schema: "clay",
                table: "GroupUser",
                column: "MembersId");
        }
    }
}
