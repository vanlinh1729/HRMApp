using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMapp.Migrations
{
    /// <inheritdoc />
    public partial class Addfieldattendentformmonth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Count",
                table: "AppAttendentForMonths",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "AppAttendentForMonths");
        }
    }
}
