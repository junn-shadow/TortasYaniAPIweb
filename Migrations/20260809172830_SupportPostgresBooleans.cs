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
            if (migrationBuilder.ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                migrationBuilder.Sql("ALTER TABLE \"Users\" ALTER COLUMN \"Activo\" TYPE boolean USING CASE WHEN \"Activo\" = 1 THEN true ELSE false END;");
                migrationBuilder.Sql("ALTER TABLE \"Users\" ALTER COLUMN \"Activo\" SET DEFAULT true;");
            }
            else
            {
                migrationBuilder.AlterColumn<bool>(
                    name: "Activo",
                    table: "Users",
                    type: "INTEGER",
                    nullable: false,
                    defaultValue: true,
                    oldClrType: typeof(int),
                    oldType: "INTEGER");
            }
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
