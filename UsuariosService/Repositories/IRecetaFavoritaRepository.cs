using UsuariosService.DTOs;
using UsuariosService.Models;


namespace UsuariosService.Repositories;

public interface IRecetaFavoritaRepository
{
    Task<IEnumerable<int>> ObtenerRecetasFavoritasAsync(int usuarioId);
    Task AgregarFavoritaAsync(int usuarioId, int recetaId);
    Task EliminarFavoritaAsync(int usuarioId, int recetaId);
}
