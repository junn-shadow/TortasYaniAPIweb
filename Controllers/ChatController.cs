using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TortasYaniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IHttpClientFactory httpClientFactory, IConfiguration config, ILogger<ChatController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
            _logger = logger;
        }

        public class ChatRequestDTO
        {
            [JsonPropertyName("messages")]
            public List<ChatMessageDTO> Messages { get; set; } = new();

            [JsonPropertyName("model")]
            public string? Model { get; set; }

            [JsonPropertyName("temperature")]
            public double? Temperature { get; set; }

            [JsonPropertyName("max_tokens")]
            public int? MaxTokens { get; set; }
        }

        public class ChatMessageDTO
        {
            [JsonPropertyName("role")]
            public string Role { get; set; } = string.Empty;

            [JsonPropertyName("content")]
            public string Content { get; set; } = string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] ChatRequestDTO dto)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY") 
                             ?? _config["Groq:ApiKey"];

                if (string.IsNullOrEmpty(apiKey) || apiKey == "YOUR_GROQ_API_KEY")
                {
                    return BadRequest(new { 
                        Success = false, 
                        Message = "La clave 'GROQ_API_KEY' no está configurada en el servidor backend." 
                    });
                }

                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey.Trim());

                var payload = new
                {
                    model = string.IsNullOrEmpty(dto.Model) ? "llama-3.3-70b-versatile" : dto.Model,
                    messages = dto.Messages,
                    temperature = dto.Temperature ?? 0.7,
                    max_tokens = dto.MaxTokens ?? 500
                };

                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var jsonPayload = JsonSerializer.Serialize(payload, jsonOptions);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api.groq.com/openai/v1/chat/completions", content);

                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error llamando a Groq API ({StatusCode}): {Response}", response.StatusCode, responseString);
                    return StatusCode((int)response.StatusCode, new { Success = false, Message = "Error de respuesta del proveedor de IA.", Detail = responseString });
                }

                using var doc = JsonDocument.Parse(responseString);
                var root = doc.RootElement;
                if (root.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
                {
                    var reply = choices[0].GetProperty("message").GetProperty("content").GetString();
                    return Ok(new { Success = true, Reply = reply });
                }

                return BadRequest(new { Success = false, Message = "Respuesta de IA sin contenido." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción en ChatController");
                return StatusCode(500, new { Success = false, Message = "Error interno procesando el chat: " + ex.Message });
            }
        }
    }
}
