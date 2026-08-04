using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TortasYaniAPI.Models;

namespace TortasYaniAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Nombre = "Torta de Chocolate",
                    Categoria = "Tortas Especiales",
                    Precio = 85.0,
                    Stock = 12,
                    Imagen = "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234559/torta_de_chocolate_wv8mi7.png",
                    Rating = 4.9,
                    Resenas = 124,
                    Badge = "Popular",
                    Descripcion = "Deliciosa torta de chocolate con capas de bizcocho húmedo y ganache.",
                    IngredientesJson = "[\"Chocolate\",\"Harina\",\"Huevos\",\"Mantequilla\",\"Azúcar\"]",
                    TamaniosJson = "[\"S\",\"M\",\"L\"]"
                },
                new Product
                {
                    Id = 2,
                    Nombre = "Cheesecake de Maracuyá",
                    Categoria = "Cheesecake y Pyes",
                    Precio = 80.0,
                    Stock = 0,
                    Imagen = "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234883/Cheesecake_de_Maracuy%C3%A1_knhn3w.png",
                    Rating = 4.9,
                    Resenas = 110,
                    Badge = "Popular",
                    Descripcion = "Refrescante cheesecake con coulis de maracuyá tropical.",
                    IngredientesJson = "[\"Maracuyá\",\"Queso crema\",\"Galletas\",\"Crema\",\"Azúcar\"]",
                    TamaniosJson = "[\"S\",\"M\",\"L\"]"
                },
                new Product
                {
                    Id = 3,
                    Nombre = "Torta de Zanahoria",
                    Categoria = "Tortas",
                    Precio = 65.0,
                    Stock = 5,
                    Imagen = "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234868/Torta_de_Zanahoriaa_ury5wh.png",
                    Rating = 4.7,
                    Resenas = 76,
                    Badge = "Favorito",
                    Descripcion = "Esponjosa torta de zanahoria con frosting de queso crema y nueces.",
                    IngredientesJson = "[\"Zanahoria\",\"Harina\",\"Huevos\",\"Nueces\",\"Queso crema\"]",
                    TamaniosJson = "[\"S\",\"M\",\"L\"]"
                },
                new Product
                {
                    Id = 4,
                    Nombre = "Torta de Vainilla",
                    Categoria = "Tortas",
                    Precio = 60.0,
                    Stock = 15,
                    Imagen = "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234876/torta_de_vainilla_vgcfkf.png",
                    Rating = 4.6,
                    Resenas = 89,
                    Badge = "",
                    Descripcion = "Clásica torta de vainilla con crema suave y decoración elegante.",
                    IngredientesJson = "[\"Vainilla\",\"Harina\",\"Huevos\",\"Mantequilla\",\"Leche\"]",
                    TamaniosJson = "[\"S\",\"M\",\"L\"]"
                },
                new Product
                {
                    Id = 5,
                    Nombre = "Torta Matrimonial",
                    Categoria = "Matrimoniales",
                    Precio = 250.0,
                    Stock = 2,
                    Imagen = "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234891/Torta_Matrimonial_qhxegx.png",
                    Rating = 5.0,
                    Resenas = 45,
                    Badge = "Premium",
                    Descripcion = "Elegante torta matrimonial de varios pisos decorada a medida.",
                    IngredientesJson = "[\"Vainilla\",\"Fondant\",\"Crema\",\"Flores\",\"Perlas\"]",
                    TamaniosJson = "[\"M\",\"L\",\"XL\"]"
                },
                new Product
                {
                    Id = 6,
                    Nombre = "Torta de Quinceañera",
                    Categoria = "Quinceañeros",
                    Precio = 200.0,
                    Stock = 3,
                    Imagen = "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234897/Torta_de_Quincea%C3%B1era_evxzmp.png",
                    Rating = 4.8,
                    Resenas = 62,
                    Badge = "Especial",
                    Descripcion = "Torta especial para quinceañeras con decoración rosa y detalles dorados.",
                    IngredientesJson = "[\"Vainilla\",\"Fondant rosa\",\"Crema\",\"Flores\",\"Brillantina\"]",
                    TamaniosJson = "[\"M\",\"L\",\"XL\"]"
                },
                new Product
                {
                    Id = 7,
                    Nombre = "Pie de Limón",
                    Categoria = "Cheesecake y Pyes",
                    Precio = 55.0,
                    Stock = 10,
                    Imagen = "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234905/Pie_de_Lim%C3%B3n_plhcyw.png",
                    Rating = 4.7,
                    Resenas = 83,
                    Badge = "",
                    Descripcion = "Clásico pie de limón con merengue tostado y base crocante.",
                    IngredientesJson = "[\"Limón\",\"Huevos\",\"Azúcar\",\"Galletas\",\"Mantequilla\"]",
                    TamaniosJson = "[\"S\",\"M\",\"L\"]"
                },
                new Product
                {
                    Id = 8,
                    Nombre = "Red Velvet",
                    Categoria = "Tortas Especiales",
                    Precio = 90.0,
                    Stock = 8,
                    Imagen = "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234910/Red_Velvet_da5fqq.png",
                    Rating = 4.9,
                    Resenas = 137,
                    Badge = "Top",
                    Descripcion = "Irresistible red velvet con frosting de queso crema y color rojo intenso.",
                    IngredientesJson = "[\"Cacao\",\"Colorante rojo\",\"Queso crema\",\"Harina\",\"Buttermilk\"]",
                    TamaniosJson = "[\"S\",\"M\",\"L\"]"
                },
                new Product
                {
                    Id = 9,
                    Nombre = "Tres Leches",
                    Categoria = "Tortas",
                    Precio = 70.0,
                    Stock = 14,
                    Imagen = "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234917/Tres_Leches_d8lm11.png",
                    Rating = 4.8,
                    Resenas = 91,
                    Badge = "Nuevo",
                    Descripcion = "Esponjoso bizcocho empapado en tres tipos de leche con crema chantilly.",
                    IngredientesJson = "[\"Leche condensada\",\"Leche evaporada\",\"Crema\",\"Huevos\",\"Harina\"]",
                    TamaniosJson = "[\"S\",\"M\",\"L\"]"
                },
                new Product
                {
                    Id = 10,
                    Nombre = "Torta de Frutos del Bosque",
                    Categoria = "Tortas Especiales",
                    Precio = 95.0,
                    Stock = 6,
                    Imagen = "https://res.cloudinary.com/ddfzttgyr/image/upload/v1774234923/Torta_de_Frutos_del_Bosque_sfpmtk.png",
                    Rating = 4.8,
                    Resenas = 72,
                    Badge = "Nuevo",
                    Descripcion = "Exquisita torta con mix de frutos del bosque frescos y crema.",
                    IngredientesJson = "[\"Frutos del bosque\",\"Crema\",\"Harina\",\"Huevos\",\"Azúcar\"]",
                    TamaniosJson = "[\"S\",\"M\",\"L\"]"
                }
            );
        }
    }
}
