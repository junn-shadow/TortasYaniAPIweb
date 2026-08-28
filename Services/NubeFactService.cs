using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TortasYaniAPI.Services
{
    public class NubeFactService
    {
        private readonly HttpClient _httpClient;
        private readonly string _ruta;
        private readonly string _token;

        public NubeFactService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _ruta = Environment.GetEnvironmentVariable("NUBEFACT_RUTA") 
                    ?? configuration["NubeFact:Ruta"] 
                    ?? "https://api.nubefact.com/api/v1/74dcf8af-0835-4cb9-bff2-e69ec2db847b";

            _token = Environment.GetEnvironmentVariable("NUBEFACT_TOKEN") 
                     ?? configuration["NubeFact:Token"] 
                     ?? "4ba047930f0a46a8b82b45cc6ae7a4c3e7c36bdf671040709ec5ac4d8cf8d916";
        }

        public async Task<string> GenerarBoletaPruebaAsync(string dniCliente, string nombreCliente, string direccionCliente, decimal totalVenta, string descripcionProducto)
        {
            decimal valorVenta = Math.Round(totalVenta / 1.18m, 2);
            decimal igv = Math.Round(totalVenta - valorVenta, 2);

            var comprobante = new
            {
                operacion = "generar_comprobante",
                tipo_de_comprobante = 2, // 2 = Boleta de Venta
                serie = "BBB1",          // Serie oficial de prueba NubeFact
                numero = 1,
                sunat_transaction = 1,
                cliente_tipo_de_documento = 1, // 1 = DNI
                cliente_numero_de_documento = string.IsNullOrWhiteSpace(dniCliente) ? "00000000" : dniCliente,
                cliente_denominacion = string.IsNullOrWhiteSpace(nombreCliente) ? "CLIENTE PUBLICO GENERAL" : nombreCliente,
                cliente_direccion = string.IsNullOrWhiteSpace(direccionCliente) ? "LIMA PERU" : direccionCliente,
                cliente_email = "",
                fecha_de_emision = DateTime.Now.ToString("dd-MM-yyyy"),
                moneda = 1, // 1 = Soles (PEN)
                porcentaje_de_igv = 18.00,
                total_gravada = valorVenta,
                total_igv = igv,
                total = totalVenta,
                detalles = new[]
                {
                    new
                    {
                        unidad_de_medida = "NIU",
                        codigo = "P001",
                        descripcion = string.IsNullOrWhiteSpace(descripcionProducto) ? "Pedido Tortas Yani" : descripcionProducto,
                        cantidad = 1,
                        valor_unitario = valorVenta,
                        precio_unitario = totalVenta,
                        subtotal = valorVenta,
                        tipo_de_igv = 1, // 1 = Gravado
                        igv = igv,
                        total = totalVenta
                    }
                }
            };

            string jsonPayload = JsonSerializer.Serialize(comprobante);

            var request = new HttpRequestMessage(HttpMethod.Post, _ruta);
            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {_token}");
            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
