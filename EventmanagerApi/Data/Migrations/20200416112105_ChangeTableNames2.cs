using Microsoft.EntityFrameworkCore.Migrations;

namespace EventmanagerApi.Data.Migrations
{
    public partial class ChangeTableNames2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "ApplicationUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ApplicationUser",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ApplicationUser");
        }
    }
}
