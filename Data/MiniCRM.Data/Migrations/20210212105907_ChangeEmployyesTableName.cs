using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniCRM.Data.Migrations
{
    public partial class ChangeEmployyesTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Employers_EmployerId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Addresses_AddressId",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_AspNetUsers_AccountId",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Companies_CompanyId",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_JobTitles_JobTitleId",
                table: "Employers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employers",
                table: "Employers");

            migrationBuilder.RenameTable(
                name: "Employers",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_Employers_JobTitleId",
                table: "Employees",
                newName: "IX_Employees_JobTitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Employers_IsDeleted",
                table: "Employees",
                newName: "IX_Employees_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Employers_CompanyId",
                table: "Employees",
                newName: "IX_Employees_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Employers_AddressId",
                table: "Employees",
                newName: "IX_Employees_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Employers_AccountId",
                table: "Employees",
                newName: "IX_Employees_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Employees_EmployerId",
                table: "Customers",
                column: "EmployerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Addresses_AddressId",
                table: "Employees",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_AccountId",
                table: "Employees",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_JobTitles_JobTitleId",
                table: "Employees",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Employees_EmployerId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Addresses_AddressId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_AccountId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_JobTitles_JobTitleId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employers");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_JobTitleId",
                table: "Employers",
                newName: "IX_Employers_JobTitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_IsDeleted",
                table: "Employers",
                newName: "IX_Employers_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CompanyId",
                table: "Employers",
                newName: "IX_Employers_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_AddressId",
                table: "Employers",
                newName: "IX_Employers_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_AccountId",
                table: "Employers",
                newName: "IX_Employers_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employers",
                table: "Employers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Employers_EmployerId",
                table: "Customers",
                column: "EmployerId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Addresses_AddressId",
                table: "Employers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_AspNetUsers_AccountId",
                table: "Employers",
                column: "AccountId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_JobTitles_JobTitleId",
                table: "Employers",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
