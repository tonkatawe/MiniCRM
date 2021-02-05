using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniCRM.Data.Migrations
{
    public partial class AddEmployerUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_AspNetUsers_ApplicationUserId",
                table: "Employers");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Employers",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Employers_ApplicationUserId",
                table: "Employers",
                newName: "IX_Employers_AccountId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Employers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_AspNetUsers_AccountId",
                table: "Employers",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_AspNetUsers_AccountId",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employers");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Employers",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employers_AccountId",
                table: "Employers",
                newName: "IX_Employers_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_AspNetUsers_ApplicationUserId",
                table: "Employers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
