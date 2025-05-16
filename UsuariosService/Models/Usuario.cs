namespace UsuariosService.Models;


public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public List<RecetaFavorita> RecetasFavoritas { get; set; } = new();

}
