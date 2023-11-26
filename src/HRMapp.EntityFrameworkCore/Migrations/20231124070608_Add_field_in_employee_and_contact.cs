using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMapp.Migrations
{
    /// <inheritdoc />
    public partial class Addfieldinemployeeandcontact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "AppSalaries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AttendentForMonthId",
                table: "AppSalaries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeePosition",
                table: "AppEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "AppContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "AppContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AppEmployeeHistories_EmployeeId",
                table: "AppEmployeeHistories",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEmployeeHistories_AppEmployees_EmployeeId",
                table: "AppEmployeeHistories",
                column: "EmployeeId",
                principalTable: "AppEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppEmployeeHistories_AppEmployees_EmployeeId",
                table: "AppEmployeeHistories");

            migrationBuilder.DropIndex(
                name: "IX_AppEmployeeHistories_EmployeeId",
                table: "AppEmployeeHistories");

            migrationBuilder.DropColumn(
                name: "EmployeePosition",
                table: "AppEmployees");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "AppContacts");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "AppContacts");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "AppSalaries",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "AttendentForMonthId",
                table: "AppSalaries",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
