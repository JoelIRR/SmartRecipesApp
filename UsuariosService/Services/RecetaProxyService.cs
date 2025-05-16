using System.Net.Http.Json;
using UsuariosService.DTOs;

namespace UsuariosService.Services
{
    public class RecetaProxyService : IRecetaProxyService
    {
        private readonly HttpClient _httpClient;

        public RecetaProxyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> ObtenerNombreRecetaAsync(int recetaId)
        {
            var response = await _httpClient.GetAsync($"/api/recetas/{recetaId}");
            if (!response.IsSuccessStatusCode)
                return null;

            var contenido = await response.Content.ReadFromJsonAsync<RecetaDTO>();
            return contenido?.Nombre;
        }

        public async Task<bool> RecetaExisteAsync(int recetaId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/recetas/{recetaId}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
