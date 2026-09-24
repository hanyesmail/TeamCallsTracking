using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamCallsTracking.Migrations
{
    /// <inheritdoc />
    public partial class createAllEntities : Migration
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CallStatus",
                table: "CallStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Call",
                table: "Call");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameTable(
                name: "CallStatus",
                newName: "CallStatuses");

            migrationBuilder.RenameTable(
                name: "Call",
                newName: "Calls");

            migrationBuilder.RenameIndex(
                name: "IX_Call_EmployeeId",
                table: "Calls",
                newName: "IX_Calls_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Call_ClientId",
                table: "Calls",
                newName: "IX_Calls_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Call_CallStatusId",
                table: "Calls",
                newName: "IX_Calls_CallStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CallStatuses",
                table: "CallStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calls",
                table: "Calls",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CallStatuses",
                table: "CallStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calls",
                table: "Calls");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameTable(
                name: "CallStatuses",
                newName: "CallStatus");

            migrationBuilder.RenameTable(
                name: "Calls",
                newName: "Call");

            migrationBuilder.RenameIndex(
                name: "IX_Calls_EmployeeId",
                table: "Call",
                newName: "IX_Call_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Calls_ClientId",
                table: "Call",
                newName: "IX_Call_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Calls_CallStatusId",
                table: "Call",
                newName: "IX_Call_CallStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CallStatus",
                table: "CallStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Call",
                table: "Call",
                column: "Id");

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
        }
    }
}
