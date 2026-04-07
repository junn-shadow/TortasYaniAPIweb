using TortasYaniAPI.Data;
using TortasYaniAPI.DTOs;
using TortasYaniAPI.Models;

namespace TortasYaniAPI.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public AuthResponseDTO Login(LoginDTO dto)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Email == dto.Email && u.Password == dto.Password);

            if (user == null)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Correo o contraseña incorrectos"
                };
            }

            return new AuthResponseDTO
            {
                Success = true,
                Message = "Login exitoso",
                Token = "token-temporal-123",
                NombreCompleto = user.NombreCompleto,
                FotoUrl = user.FotoUrl
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
                NombreCompleto = dto.NombreCompleto,
                Email = dto.Email,
                Password = dto.Password,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                FotoUrl = dto.FotoUrl
            };

            _context.Users.Add(nuevoUsuario);
            _context.SaveChanges();

            return new AuthResponseDTO
            {
                Success = true,
                Message = "Usuario registrado correctamente",
                NombreCompleto = dto.NombreCompleto,
                FotoUrl = dto.FotoUrl
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
                user.Password = dto.NuevaPassword;
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
    }
}
