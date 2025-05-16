using UsuariosService.Models;

namespace UsuariosService.Repositories;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> ObtenerTodosAsync();
    Task<Usuario?> ObtenerPorEmailAsync(string email);
    Task<Usuario?> ObtenerPorIdAsync(int id);
    Task CrearAsync(Usuario usuario);
}
