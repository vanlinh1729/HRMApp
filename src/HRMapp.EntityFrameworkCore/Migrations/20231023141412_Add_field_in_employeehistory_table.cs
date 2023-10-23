using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMapp.Migrations
{
    /// <inheritdoc />
    public partial class Addfieldinemployeehistorytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "AppEmployeeHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AppEmployeeHistories");
        }
    }
}
