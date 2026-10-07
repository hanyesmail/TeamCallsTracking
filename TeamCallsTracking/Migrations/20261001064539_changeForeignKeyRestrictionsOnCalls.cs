using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamCallsTracking.Migrations
{
    /// <inheritdoc />
    public partial class changeForeignKeyRestrictionsOnCalls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calls_CallStatuses_CallStatusId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Calls_Clients_ClientId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Calls_Employees_EmployeeId",
                table: "Calls");

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_CallStatuses_CallStatusId",
                table: "Calls",
                column: "CallStatusId",
                principalTable: "CallStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_Clients_ClientId",
                table: "Calls",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_Employees_EmployeeId",
                table: "Calls",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calls_CallStatuses_CallStatusId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Calls_Clients_ClientId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Calls_Employees_EmployeeId",
                table: "Calls");

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_CallStatuses_CallStatusId",
                table: "Calls",
                column: "CallStatusId",
                principalTable: "CallStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_Clients_ClientId",
                table: "Calls",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_Employees_EmployeeId",
                table: "Calls",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
