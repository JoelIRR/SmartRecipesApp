using UsuariosService.DTOs;


namespace UsuariosService.Services
{
    public interface IRecetaFavoritaService
    {
        Task<IEnumerable<RecetaFavoritaDTO>> ObtenerFavoritasPorUsuarioAsync(int usuarioId);
        Task<RecetaFavoritaDTO> AgregarFavoritaAsync(int usuarioId, int recetaId);
        Task EliminarFavoritaAsync(int usuarioId, int recetaId);
    }
}
