using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class userRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "User");

            migrationBuilder.AddColumn<short>(
                name: "Role",
                table: "User",
                type: "smallint",
                nullable: true);
        }
    }
}
