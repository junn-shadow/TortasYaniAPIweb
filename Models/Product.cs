namespace TortasYaniAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public double Precio { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public double Rating { get; set; } = 5.0;
        public int Resenas { get; set; } = 0;
        public string? Badge { get; set; }
        public string IngredientesJson { get; set; } = "[]";
        public string TamaniosJson { get; set; } = "[\"S\", \"M\", \"L\"]";
    }
}
