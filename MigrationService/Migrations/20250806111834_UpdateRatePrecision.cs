using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRatePrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "currency",
                type: "numeric(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,4)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "currency",
                type: "numeric(10,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,6)");
        }
    }
}
