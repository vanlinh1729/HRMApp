using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMapp.Migrations
{
    /// <inheritdoc />
    public partial class Addfieldemployeehistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobPosition",
                table: "AppEmployeeHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobPosition",
                table: "AppEmployeeHistories");
        }
    }
}
