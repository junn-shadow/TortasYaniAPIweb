using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TortasYaniAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixPostgresIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Detectar si corremos en PostgreSQL para aplicar el fix de identity
            if (migrationBuilder.ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                // Crear una secuencia para el Id y configurarla como identity
                migrationBuilder.Sql(@"
                    CREATE SEQUENCE IF NOT EXISTS ""Users_Id_seq"";
                    ALTER TABLE ""Users"" ALTER COLUMN ""Id"" SET DEFAULT nextval('""Users_Id_seq""');
                    SELECT setval('""Users_Id_seq""', COALESCE((SELECT MAX(""Id"") FROM ""Users""), 0) + 1, false);
                    ALTER TABLE ""Users"" ALTER COLUMN ""Id"" SET NOT NULL;
                ");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                migrationBuilder.Sql(@"
                    ALTER TABLE ""Users"" ALTER COLUMN ""Id"" DROP DEFAULT;
                    DROP SEQUENCE IF EXISTS ""Users_Id_seq"";
                ");
            }
        }
    }
}
