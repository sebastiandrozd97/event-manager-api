using Microsoft.EntityFrameworkCore.Migrations;

namespace EventmanagerApi.Data.Migrations
{
    public partial class ChangeTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_OrganizedEvents_EventId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizedEvents_ApplicationUsers_UserId",
                table: "OrganizedEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_OrganizedEvents_EventId",
                table: "Participant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizedEvents",
                table: "OrganizedEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUsers",
                table: "ApplicationUsers");

            migrationBuilder.RenameTable(
                name: "OrganizedEvents",
                newName: "OrganizedEvent");

            migrationBuilder.RenameTable(
                name: "ApplicationUsers",
                newName: "ApplicationUser");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizedEvents_UserId",
                table: "OrganizedEvent",
                newName: "IX_OrganizedEvent_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizedEvent",
                table: "OrganizedEvent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_OrganizedEvent_EventId",
                table: "Expense",
                column: "EventId",
                principalTable: "OrganizedEvent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizedEvent_ApplicationUser_UserId",
                table: "OrganizedEvent",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_OrganizedEvent_EventId",
                table: "Participant",
                column: "EventId",
                principalTable: "OrganizedEvent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_OrganizedEvent_EventId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizedEvent_ApplicationUser_UserId",
                table: "OrganizedEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_OrganizedEvent_EventId",
                table: "Participant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizedEvent",
                table: "OrganizedEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser");

            migrationBuilder.RenameTable(
                name: "OrganizedEvent",
                newName: "OrganizedEvents");

            migrationBuilder.RenameTable(
                name: "ApplicationUser",
                newName: "ApplicationUsers");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizedEvent_UserId",
                table: "OrganizedEvents",
                newName: "IX_OrganizedEvents_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizedEvents",
                table: "OrganizedEvents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUsers",
                table: "ApplicationUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_OrganizedEvents_EventId",
                table: "Expense",
                column: "EventId",
                principalTable: "OrganizedEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizedEvents_ApplicationUsers_UserId",
                table: "OrganizedEvents",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_OrganizedEvents_EventId",
                table: "Participant",
                column: "EventId",
                principalTable: "OrganizedEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
