using Microsoft.AspNetCore.Mvc;
using TortasYaniAPI.DTOs;
using TortasYaniAPI.Services;
using TortasYaniAPI.Data;

namespace TortasYaniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AppDbContext context)
        {
            _authService = new AuthService(context);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            var result = _authService.Login(dto);
            if (!result.Success)
                return Unauthorized(result);
            return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO dto)
        {
            var result = _authService.Register(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateDTO dto)
        {
            var result = _authService.Update(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}