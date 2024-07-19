﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClayBackend.Migrations
{
    /// <inheritdoc />
    public partial class addedCanMonitor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanMonitor",
                schema: "clay",
                table: "Groups",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanMonitor",
                schema: "clay",
                table: "Groups");
        }
    }
}
