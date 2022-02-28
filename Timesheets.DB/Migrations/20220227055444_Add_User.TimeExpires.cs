using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheets.DB.Migrations
{
    public partial class Add_UserTimeExpires : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeLastRefresh",
                table: "Users",
                newName: "TimeExpires");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeExpires",
                table: "Users",
                newName: "TimeLastRefresh");
        }
    }
}
