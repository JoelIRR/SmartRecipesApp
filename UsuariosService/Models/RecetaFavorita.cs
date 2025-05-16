namespace UsuariosService.Models;

public class RecetaFavorita
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int RecetaId { get; set; }

    public Usuario? Usuario { get; set; }
}
