using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheets.DB.Migrations
{
    public partial class UserAddTimeLastRefresh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TimeLastRefresh",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeLastRefresh",
                table: "Users");
        }
    }
}
