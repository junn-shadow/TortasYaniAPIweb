namespace TortasYaniAPI.Models.Dto;

public class CreateUserDto {
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Rol { get; set; } = "client";
    public bool Activo { get; set; } = true;
    public string Descripcion { get; set; } = string.Empty;
    public string? Password { get; set; }
}
