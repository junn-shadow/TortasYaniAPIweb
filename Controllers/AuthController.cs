using Microsoft.AspNetCore.Mvc;
using TortasYaniAPI.DTOs;
using TortasYaniAPI.Services;
using TortasYaniAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace TortasYaniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            try
            {
                var result = _authService.Login(dto);
                if (!result.Success)
                    return Unauthorized(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en login");
                return StatusCode(500, new { Success = false, Message = "Error interno del servidor: " + ex.Message });
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO dto)
        {
            try
            {
                var result = _authService.Register(dto);
                if (!result.Success)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en registro");
                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, new { Success = false, Message = "Error interno del servidor: " + message });
            }
        }

        [Authorize]
        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateDTO dto)
        {
            try
            {
                var result = _authService.Update(dto);
                if (!result.Success)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en actualización");
                return StatusCode(500, new { Success = false, Message = "Error interno del servidor: " + ex.Message });
            }
        }
    }
}