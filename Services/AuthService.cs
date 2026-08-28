using Microsoft.EntityFrameworkCore;
using TortasYaniAPI.Data;
using TortasYaniAPI.DTOs;
using TortasYaniAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TortasYaniAPI.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public AuthResponseDTO Login(LoginDTO dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Correo o contraseña incorrectos"
                };
            }

            var token = GenerarJwtToken(user);

            return new AuthResponseDTO
            {
                Success = true,
                Message = "Login exitoso",
                Token = token,
                NombreCompleto = user.NombreCompleto,
                FotoUrl = user.FotoUrl,
                Telefono = user.Telefono,
                Direccion = user.Direccion
            };
        }

        public AuthResponseDTO Register(RegisterDTO dto)
        {
            var existe = _context.Users.Any(u => u.Email == dto.Email);

            if (existe)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Este correo ya está registrado"
                };
            }

            var nuevoUsuario = new User
            {
                NombreCompleto = dto.NombreCompleto ?? "",
                Email = dto.Email ?? "",
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password ?? ""),
                Telefono = dto.Telefono ?? "",
                Direccion = dto.Direccion ?? "",
                FotoUrl = dto.FotoUrl ?? "",
                Rol = "client",
                Activo = true,
                Descripcion = ""
            };

            _context.Users.Add(nuevoUsuario);
            
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                if (_context.Database.IsNpgsql())
                {
                    _context.Database.ExecuteSqlRaw(@"
                        DO $$
                        BEGIN
                            CREATE SEQUENCE IF NOT EXISTS ""Users_Id_seq"";
                            ALTER TABLE ""Users"" ALTER COLUMN ""Id"" SET DEFAULT nextval('""Users_Id_seq""');
                            PERFORM setval('""Users_Id_seq""', COALESCE((SELECT MAX(""Id"") FROM ""Users""), 1));
                        END $$;
                    ");
                    _context.SaveChanges();
                }
                else
                {
                    throw;
                }
            }

            return new AuthResponseDTO
            {
                Success = true,
                Message = "Usuario registrado correctamente",
                NombreCompleto = nuevoUsuario.NombreCompleto,
                FotoUrl = nuevoUsuario.FotoUrl,
                Telefono = nuevoUsuario.Telefono,
                Direccion = nuevoUsuario.Direccion
            };
        }

        public AuthResponseDTO Update(UpdateDTO dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Usuario no encontrado"
                };
            }

            user.NombreCompleto = dto.NombreCompleto;
            user.Telefono = dto.Telefono;
            user.Direccion = dto.Direccion;
            user.FotoUrl = dto.FotoUrl;

            if (!string.IsNullOrEmpty(dto.NuevaPassword))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NuevaPassword);
            }

            _context.SaveChanges();

            return new AuthResponseDTO
            {
                Success = true,
                Message = "Perfil actualizado correctamente",
                NombreCompleto = user.NombreCompleto,
                FotoUrl = user.FotoUrl
            };
        }

        private string GenerarJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "KeySuperSecretTemporal2024PorqueNoEstabaEnEnv0123"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
