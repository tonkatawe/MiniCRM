using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniCRM.Data.Migrations
{
    public partial class AddHasAccountEmployer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_AspNetUsers_UserId",
                table: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Employers",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Employers_UserId",
                table: "Employers",
                newName: "IX_Employers_CompanyId");

            migrationBuilder.AlterColumn<int>(
                name: "JobTitleId",
                table: "Employers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Employers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAccount",
                table: "Employers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Employers_ApplicationUserId",
                table: "Employers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_AspNetUsers_ApplicationUserId",
                table: "Employers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Companies_CompanyId",
                table: "Employers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_AspNetUsers_ApplicationUserId",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Companies_CompanyId",
                table: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_Employers_ApplicationUserId",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "HasAccount",
                table: "Employers");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Employers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employers_CompanyId",
                table: "Employers",
                newName: "IX_Employers_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "JobTitleId",
                table: "Employers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "Employers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_AspNetUsers_UserId",
                table: "Employers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
