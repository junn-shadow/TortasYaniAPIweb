using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TortasYaniAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", nullable: false),
                    Precio = table.Column<double>(type: "REAL", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    Imagen = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<double>(type: "REAL", nullable: false),
                    Resenas = table.Column<int>(type: "INTEGER", nullable: false),
                    Badge = table.Column<string>(type: "TEXT", nullable: true),
                    IngredientesJson = table.Column<string>(type: "TEXT", nullable: false),
                    TamaniosJson = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Badge", "Categoria", "Descripcion", "Imagen", "IngredientesJson", "Nombre", "Precio", "Rating", "Resenas", "Stock", "TamaniosJson" },
                values: new object[,]
                {
                    { 1, "Popular", "Tortas Especiales", "Deliciosa torta de chocolate con capas de bizcocho húmedo y ganache.", "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234559/torta_de_chocolate_wv8mi7.png", "[\"Chocolate\",\"Harina\",\"Huevos\",\"Mantequilla\",\"Azúcar\"]", "Torta de Chocolate", 85.0, 4.9000000000000004, 124, 12, "[\"S\",\"M\",\"L\"]" },
                    { 2, "Popular", "Cheesecake y Pyes", "Refrescante cheesecake con coulis de maracuyá tropical.", "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234883/Cheesecake_de_Maracuy%C3%A1_knhn3w.png", "[\"Maracuyá\",\"Queso crema\",\"Galletas\",\"Crema\",\"Azúcar\"]", "Cheesecake de Maracuyá", 80.0, 4.9000000000000004, 110, 0, "[\"S\",\"M\",\"L\"]" },
                    { 3, "Favorito", "Tortas", "Esponjosa torta de zanahoria con frosting de queso crema y nueces.", "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234868/Torta_de_Zanahoriaa_ury5wh.png", "[\"Zanahoria\",\"Harina\",\"Huevos\",\"Nueces\",\"Queso crema\"]", "Torta de Zanahoria", 65.0, 4.7000000000000002, 76, 5, "[\"S\",\"M\",\"L\"]" },
                    { 4, "", "Tortas", "Clásica torta de vainilla con crema suave y decoración elegante.", "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234876/torta_de_vainilla_vgcfkf.png", "[\"Vainilla\",\"Harina\",\"Huevos\",\"Mantequilla\",\"Leche\"]", "Torta de Vainilla", 60.0, 4.5999999999999996, 89, 15, "[\"S\",\"M\",\"L\"]" },
                    { 5, "Premium", "Matrimoniales", "Elegante torta matrimonial de varios pisos decorada a medida.", "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234891/Torta_Matrimonial_qhxegx.png", "[\"Vainilla\",\"Fondant\",\"Crema\",\"Flores\",\"Perlas\"]", "Torta Matrimonial", 250.0, 5.0, 45, 2, "[\"M\",\"L\",\"XL\"]" },
                    { 6, "Especial", "Quinceañeros", "Torta especial para quinceañeras con decoración rosa y detalles dorados.", "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234897/Torta_de_Quincea%C3%B1era_evxzmp.png", "[\"Vainilla\",\"Fondant rosa\",\"Crema\",\"Flores\",\"Brillantina\"]", "Torta de Quinceañera", 200.0, 4.7999999999999998, 62, 3, "[\"M\",\"L\",\"XL\"]" },
                    { 7, "", "Cheesecake y Pyes", "Clásico pie de limón con merengue tostado y base crocante.", "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234905/Pie_de_Lim%C3%B3n_plhcyw.png", "[\"Limón\",\"Huevos\",\"Azúcar\",\"Galletas\",\"Mantequilla\"]", "Pie de Limón", 55.0, 4.7000000000000002, 83, 10, "[\"S\",\"M\",\"L\"]" },
                    { 8, "Top", "Tortas Especiales", "Irresistible red velvet con frosting de queso crema y color rojo intenso.", "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234910/Red_Velvet_da5fqq.png", "[\"Cacao\",\"Colorante rojo\",\"Queso crema\",\"Harina\",\"Buttermilk\"]", "Red Velvet", 90.0, 4.9000000000000004, 137, 8, "[\"S\",\"M\",\"L\"]" },
                    { 9, "Nuevo", "Tortas", "Esponjoso bizcocho empapado en tres tipos de leche con crema chantilly.", "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234917/Tres_Leches_d8lm11.png", "[\"Leche condensada\",\"Leche evaporada\",\"Crema\",\"Huevos\",\"Harina\"]", "Tres Leches", 70.0, 4.7999999999999998, 91, 14, "[\"S\",\"M\",\"L\"]" },
                    { 10, "Nuevo", "Tortas Especiales", "Exquisita torta con mix de frutos del bosque frescos y crema.", "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234923/Torta_de_Frutos_del_Bosque_sfpmtk.png", "[\"Frutos del bosque\",\"Crema\",\"Harina\",\"Huevos\",\"Azúcar\"]", "Torta de Frutos del Bosque", 95.0, 4.7999999999999998, 72, 6, "[\"S\",\"M\",\"L\"]" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
