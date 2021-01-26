using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniCRM.Data.Migrations
{
    public partial class AddEmoployerNavigationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "PhoneNumbers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "EmailAddresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_EmployerId",
                table: "PhoneNumbers",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_EmployerId",
                table: "EmailAddresses",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddresses_Employer_EmployerId",
                table: "EmailAddresses",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Employer_EmployerId",
                table: "PhoneNumbers",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddresses_Employer_EmployerId",
                table: "EmailAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Employer_EmployerId",
                table: "PhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_PhoneNumbers_EmployerId",
                table: "PhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_EmailAddresses_EmployerId",
                table: "EmailAddresses");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "EmailAddresses");
        }
    }
}
