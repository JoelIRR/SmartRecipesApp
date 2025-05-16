namespace UsuariosService.DTOs;

public class UsuarioFavoritasDTO
{
    public string NombreUsuario { get; set; } = string.Empty;
    public List<string> RecetasFavoritas { get; set; } = new();
}
