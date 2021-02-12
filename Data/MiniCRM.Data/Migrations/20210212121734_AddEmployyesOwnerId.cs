using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniCRM.Data.Migrations
{
    public partial class AddEmployyesOwnerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_AccountId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AccountId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OwnerId",
                table: "Employees",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_OwnerId",
                table: "Employees",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_OwnerId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_OwnerId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AccountId",
                table: "Employees",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_AccountId",
                table: "Employees",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
