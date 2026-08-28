using System.Text.Json.Serialization;
using TortasYaniAPI.Models;

namespace TortasYaniAPI.Models.Dto;

public class UserDto {
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = string.Empty;
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("rol")]
    public string Rol { get; set; } = "client";
    [JsonPropertyName("activo")]
    public bool Activo { get; set; } = true;
    [JsonPropertyName("descripcion")]
    public string Descripcion { get; set; } = string.Empty;
    public UserDto() {}
    public UserDto(User u) {
        Id = u.Id;
        Nombre = u.NombreCompleto;
        Email = u.Email;
        Rol = u.Rol;
        Activo = u.Activo;
        Descripcion = u.Descripcion;
    }
}
