using UsuariosService.Models;
using UsuariosService.Repositories;
using UsuariosService.DTOs;

namespace UsuariosService.Services
{
    public class RecetaFavoritaService : IRecetaFavoritaService
    {
        private readonly IRecetaFavoritaRepository _repository;

        public RecetaFavoritaService(IRecetaFavoritaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RecetaFavoritaDTO>> ObtenerFavoritasPorUsuarioAsync(int usuarioId)
        {
            var recetasIds = await _repository.ObtenerRecetasFavoritasAsync(usuarioId);
            return recetasIds.Select(id => new RecetaFavoritaDTO
            {
                UsuarioId = usuarioId,
                RecetaId = id
            });
        }

        public async Task<RecetaFavoritaDTO> AgregarFavoritaAsync(int usuarioId, int recetaId)
        {
            await _repository.AgregarFavoritaAsync(usuarioId, recetaId);
            return new RecetaFavoritaDTO
            {
                UsuarioId = usuarioId,
                RecetaId = recetaId
            };
        }

        public async Task EliminarFavoritaAsync(int usuarioId, int recetaId)
        {
            await _repository.EliminarFavoritaAsync(usuarioId, recetaId);
        }
    }
}
