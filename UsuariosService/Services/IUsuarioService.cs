using UsuariosService.Models;

namespace UsuariosService.Services;

public interface IUsuarioService
{
    Task<Usuario?> AutenticarAsync(string email, string password);
    Task<Usuario> RegistrarAsync(string nombre, string email, string password);
    Task<Usuario?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<Usuario>> ObtenerTodosAsync();
    
}
