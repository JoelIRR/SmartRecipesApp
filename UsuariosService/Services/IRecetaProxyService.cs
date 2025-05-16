namespace UsuariosService.Services
{
    public interface IRecetaProxyService
    {
        Task<bool> RecetaExisteAsync(int recetaId);
        Task<string?> ObtenerNombreRecetaAsync(int recetaId);

    }
}
