using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClayBackend.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermission_Doors_DoorId",
                schema: "clay",
                table: "GroupPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermission_Groups_GroupId",
                schema: "clay",
                table: "GroupPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetUsers_UserId",
                schema: "clay",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Doors_DoorId",
                schema: "clay",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                schema: "clay",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermission",
                schema: "clay",
                table: "GroupPermission");

            migrationBuilder.RenameTable(
                name: "Permissions",
                schema: "clay",
                newName: "UserPermissions",
                newSchema: "clay");

            migrationBuilder.RenameTable(
                name: "GroupPermission",
                schema: "clay",
                newName: "GroupPermissions",
                newSchema: "clay");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_UserId",
                schema: "clay",
                table: "UserPermissions",
                newName: "IX_UserPermissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupPermission_GroupId",
                schema: "clay",
                table: "GroupPermissions",
                newName: "IX_GroupPermissions_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissions",
                schema: "clay",
                table: "UserPermissions",
                columns: new[] { "DoorId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermissions",
                schema: "clay",
                table: "GroupPermissions",
                columns: new[] { "DoorId", "GroupId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_Doors_DoorId",
                schema: "clay",
                table: "GroupPermissions",
                column: "DoorId",
                principalSchema: "clay",
                principalTable: "Doors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_Groups_GroupId",
                schema: "clay",
                table: "GroupPermissions",
                column: "GroupId",
                principalSchema: "clay",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_AspNetUsers_UserId",
                schema: "clay",
                table: "UserPermissions",
                column: "UserId",
                principalSchema: "clay",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Doors_DoorId",
                schema: "clay",
                table: "UserPermissions",
                column: "DoorId",
                principalSchema: "clay",
                principalTable: "Doors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_Doors_DoorId",
                schema: "clay",
                table: "GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_Groups_GroupId",
                schema: "clay",
                table: "GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_AspNetUsers_UserId",
                schema: "clay",
                table: "UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Doors_DoorId",
                schema: "clay",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissions",
                schema: "clay",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermissions",
                schema: "clay",
                table: "GroupPermissions");

            migrationBuilder.RenameTable(
                name: "UserPermissions",
                schema: "clay",
                newName: "Permissions",
                newSchema: "clay");

            migrationBuilder.RenameTable(
                name: "GroupPermissions",
                schema: "clay",
                newName: "GroupPermission",
                newSchema: "clay");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_UserId",
                schema: "clay",
                table: "Permissions",
                newName: "IX_Permissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupPermissions_GroupId",
                schema: "clay",
                table: "GroupPermission",
                newName: "IX_GroupPermission_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                schema: "clay",
                table: "Permissions",
                columns: new[] { "DoorId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermission",
                schema: "clay",
                table: "GroupPermission",
                columns: new[] { "DoorId", "GroupId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermission_Doors_DoorId",
                schema: "clay",
                table: "GroupPermission",
                column: "DoorId",
                principalSchema: "clay",
                principalTable: "Doors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermission_Groups_GroupId",
                schema: "clay",
                table: "GroupPermission",
                column: "GroupId",
                principalSchema: "clay",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetUsers_UserId",
                schema: "clay",
                table: "Permissions",
                column: "UserId",
                principalSchema: "clay",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Doors_DoorId",
                schema: "clay",
                table: "Permissions",
                column: "DoorId",
                principalSchema: "clay",
                principalTable: "Doors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
