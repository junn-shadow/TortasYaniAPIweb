using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TortasYaniAPI.Services;

namespace TortasYaniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturacionController : ControllerBase
    {
        private readonly NubeFactService _nubeFactService;

        public FacturacionController(NubeFactService nubeFactService)
        {
            _nubeFactService = nubeFactService;
        }

        public class EmisionRequest
        {
            public string DniCliente { get; set; } = "00000000";
            public string NombreCliente { get; set; } = "CLIENTE PRUEBA";
            public string DireccionCliente { get; set; } = "LIMA PERU";
            public decimal TotalVenta { get; set; } = 50.00m;
            public string DescripcionProducto { get; set; } = "Torta Especial Yani";
        }

        [HttpPost("emitir-boleta")]
        public async Task<IActionResult> EmitirBoleta([FromBody] EmisionRequest req)
        {
            try
            {
                string jsonRespuesta = await _nubeFactService.GenerarBoletaPruebaAsync(
                    req.DniCliente,
                    req.NombreCliente,
                    req.DireccionCliente,
                    req.TotalVenta,
                    req.DescripcionProducto
                );

                return Content(jsonRespuesta, "application/json");
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
