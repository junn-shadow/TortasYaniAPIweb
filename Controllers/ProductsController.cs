using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TortasYaniAPI.Data;
using TortasYaniAPI.DTOs;
using TortasYaniAPI.Models;

namespace TortasYaniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(AppDbContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Helper method to convert Product to ProductDTO
        private static ProductDTO MapToDTO(Product product)
        {
            List<string> ingredientes = new();
            List<string> tamanios = new();

            try
            {
                ingredientes = JsonSerializer.Deserialize<List<string>>(product.IngredientesJson) ?? new();
            }
            catch
            {
                // Fallback if not valid JSON
                ingredientes = new List<string> { product.IngredientesJson };
            }

            try
            {
                tamanios = JsonSerializer.Deserialize<List<string>>(product.TamaniosJson) ?? new();
            }
            catch
            {
                tamanios = new List<string> { "S", "M", "L" };
            }

            return new ProductDTO
            {
                Id = product.Id,
                Nombre = product.Nombre,
                Categoria = product.Categoria,
                Precio = product.Precio,
                Stock = product.Stock,
                Imagen = product.Imagen,
                Descripcion = product.Descripcion,
                Rating = product.Rating,
                Resenas = product.Resenas,
                Badge = product.Badge,
                Ingredientes = ingredientes,
                Tamanios = tamanios
            };
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts([FromQuery] string? category)
        {
            try
            {
                IQueryable<Product> query = _context.Products;

                if (!string.IsNullOrEmpty(category))
                {
                    query = query.Where(p => p.Categoria.ToLower() == category.ToLower());
                }

                var products = await query.ToListAsync();
                return Ok(products.Select(MapToDTO));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos");
                return StatusCode(500, new { Success = false, Message = "Error interno del servidor" });
            }
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound(new { Success = false, Message = $"Producto con id {id} no encontrado" });
                }

                return Ok(MapToDTO(product));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener producto con id {Id}", id);
                return StatusCode(500, new { Success = false, Message = "Error interno del servidor" });
            }
        }

        // POST: api/products
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] ProductCreateDTO dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Nombre) || string.IsNullOrEmpty(dto.Categoria) || dto.Precio < 0)
                {
                    return BadRequest(new { Success = false, Message = "Datos del producto inválidos" });
                }

                var product = new Product
                {
                    Nombre = dto.Nombre,
                    Categoria = dto.Categoria,
                    Precio = dto.Precio,
                    Stock = dto.Stock,
                    Imagen = dto.Imagen,
                    Descripcion = dto.Descripcion,
                    Badge = dto.Badge,
                    Rating = 5.0,
                    Resenas = 0,
                    IngredientesJson = JsonSerializer.Serialize(dto.Ingredientes),
                    TamaniosJson = JsonSerializer.Serialize(dto.Tamanios)
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                var createdDTO = MapToDTO(product);
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, createdDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto");
                return StatusCode(500, new { Success = false, Message = "Error interno del servidor" });
            }
        }

        // PUT: api/products/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDTO dto)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound(new { Success = false, Message = $"Producto con id {id} no encontrado" });
                }

                product.Nombre = dto.Nombre;
                product.Categoria = dto.Categoria;
                product.Precio = dto.Precio;
                product.Stock = dto.Stock;
                product.Imagen = dto.Imagen;
                product.Descripcion = dto.Descripcion;
                product.Badge = dto.Badge;
                product.IngredientesJson = JsonSerializer.Serialize(dto.Ingredientes);
                product.TamaniosJson = JsonSerializer.Serialize(dto.Tamanios);

                await _context.SaveChangesAsync();

                return Ok(MapToDTO(product));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar producto con id {Id}", id);
                return StatusCode(500, new { Success = false, Message = "Error interno del servidor" });
            }
        }

        // DELETE: api/products/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound(new { Success = false, Message = $"Producto con id {id} no encontrado" });
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return Ok(new { Success = true, Message = "Producto eliminado correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar producto con id {Id}", id);
                return StatusCode(500, new { Success = false, Message = "Error interno del servidor" });
            }
        }
    }
}
