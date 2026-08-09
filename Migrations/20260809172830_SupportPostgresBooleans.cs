using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TortasYaniAPI.Migrations
{
    /// <inheritdoc />
    public partial class SupportPostgresBooleans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Forzar alteración de columna para que pase de INTEGER a boolean en PostgreSQL
            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Activo",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }
    }
}
