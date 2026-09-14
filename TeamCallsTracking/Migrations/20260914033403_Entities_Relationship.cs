using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamCallsTracking.Migrations
{
    /// <inheritdoc />
    public partial class Entities_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Call_CallStatus_CallStatusId",
                table: "Call");

            migrationBuilder.DropForeignKey(
                name: "FK_Call_Client_ClientId",
                table: "Call");

            migrationBuilder.DropForeignKey(
                name: "FK_Call_Employees_EmployeeId",
                table: "Call");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTitles_EmployeeTitleId",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Call_CallStatus_CallStatusId",
                table: "Call",
                column: "CallStatusId",
                principalTable: "CallStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Call_Client_ClientId",
                table: "Call",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Call_Employees_EmployeeId",
                table: "Call",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTitles_EmployeeTitleId",
                table: "Employees",
                column: "EmployeeTitleId",
                principalTable: "EmployeeTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Call_CallStatus_CallStatusId",
                table: "Call");

            migrationBuilder.DropForeignKey(
                name: "FK_Call_Client_ClientId",
                table: "Call");

            migrationBuilder.DropForeignKey(
                name: "FK_Call_Employees_EmployeeId",
                table: "Call");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTitles_EmployeeTitleId",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Call_CallStatus_CallStatusId",
                table: "Call",
                column: "CallStatusId",
                principalTable: "CallStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Call_Client_ClientId",
                table: "Call",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Call_Employees_EmployeeId",
                table: "Call",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTitles_EmployeeTitleId",
                table: "Employees",
                column: "EmployeeTitleId",
                principalTable: "EmployeeTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
