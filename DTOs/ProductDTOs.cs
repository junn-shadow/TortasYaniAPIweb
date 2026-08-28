using System.Collections.Generic;

namespace TortasYaniAPI.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public double Precio { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int Resenas { get; set; }
        public string? Badge { get; set; }
        public List<string> Ingredientes { get; set; } = new();
        public List<string> Tamanios { get; set; } = new();
    }

    public class ProductCreateDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public double Precio { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Badge { get; set; }
        public List<string> Ingredientes { get; set; } = new();
        public List<string> Tamanios { get; set; } = new();
    }

    public class ProductUpdateDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public double Precio { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Badge { get; set; }
        public List<string> Ingredientes { get; set; } = new();
        public List<string> Tamanios { get; set; } = new();
    }
}
