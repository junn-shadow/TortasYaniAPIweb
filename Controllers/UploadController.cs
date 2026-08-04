using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TortasYaniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IConfiguration _config;
        public UploadController(IConfiguration config) => _config = config;

        public class SignatureRequest
        {
            public string? Folder { get; set; }
        }

        [HttpPost("signature")]
        public IActionResult GetSignature([FromBody] SignatureRequest request)
        {
            var cloudName = _config["Cloudinary:CloudName"];
            var apiKey    = _config["Cloudinary:ApiKey"];
            var apiSecret = _config["Cloudinary:ApiSecret"];

            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            var folder = request.Folder ?? "admin/products";

            // Parameters that must be signed (ordered alphabetically)
            var parameters = new SortedDictionary<string, string>
            {
                { "folder", folder },
                { "timestamp", timestamp }
            };

            // Build the string "key=value&key=value..."
            var toSign = string.Join("&", parameters.Select(kv => $"{kv.Key}={kv.Value}"));
            // Append the API secret and compute SHA1
            using var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(toSign + apiSecret));
            var signature = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

            var response = new
            {
                url = $"https://api.cloudinary.com/v1_1/{cloudName}/image/upload",
                @params = new
                {
                    api_key = apiKey,
                    timestamp = timestamp,
                    signature = signature,
                    folder = folder
                }
            };

            return Ok(response);
        }
    }
}
