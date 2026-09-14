using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamCallsTracking.Migrations
{
    /// <inheritdoc />
    public partial class add_call_duration_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationInSeconds",
                table: "Call",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInSeconds",
                table: "Call");
        }
    }
}
