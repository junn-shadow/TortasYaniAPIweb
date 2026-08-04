using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TortasYaniAPI.Data;
using TortasYaniAPI.Models;
using TortasYaniAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace TortasYaniAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    public UsersController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _context.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                Nombre = u.NombreCompleto,
                Email = u.Email,
                Rol = u.Rol,
                Activo = u.Activo,
                Descripcion = u.Descripcion
            })
            .ToListAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto dto)
    {
        var user = new User
        {
            NombreCompleto = dto.Nombre,
            Email = dto.Email,
            Rol = dto.Rol,
            Activo = dto.Activo,
            Descripcion = dto.Descripcion,
            Password = dto.Password != null ? BCrypt.Net.BCrypt.HashPassword(dto.Password) : string.Empty
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var createdDto = new UserDto(user);
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, createdDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();
        user.NombreCompleto = dto.Nombre;
        user.Email = dto.Email;
        user.Rol = dto.Rol;
        user.Activo = dto.Activo;
        user.Descripcion = dto.Descripcion;
        await _context.SaveChangesAsync();
        return Ok(new UserDto(user));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
