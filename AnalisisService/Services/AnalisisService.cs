using System.Text.Json;
using AnalisisService.Models;
using AnalisisService.DTOs;

namespace AnalisisService.Services;

public class AnalisisService : IAnalisisService
{
    
    private readonly HttpClient _httpClient;

     // Inyección del cliente HTTP para hacer peticiones al microservicio de Recetas
    public AnalisisService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

     // Método que obtiene una receta, consulta cada ingrediente, calcula las calorías y retorna el resultado
    public async Task<RecetaResultado?> ObtenerRecetaConCaloriasAsync(int recetaId)
    {
        // Obtener receta desde el microservicio de recetas
        var recetaResponse = await _httpClient.GetAsync($"/api/recetas/{recetaId}");
        if (!recetaResponse.IsSuccessStatusCode) return null;

        var recetaJson = await recetaResponse.Content.ReadAsStringAsync();
        var receta = JsonSerializer.Deserialize<RecetaDTO>(recetaJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (receta == null) return null;

        double totalCalorias = 0;
        var ingredientes = new List<IngredienteInfo>();

         // Recorre todos los ingredientes asociados a la receta
        foreach (var ingrediente in receta.Ingredientes)
        {
            // información completa del ingrediente
            var ingredienteResponse = await _httpClient.GetAsync($"/api/ingredientes/{ingrediente.IngredienteId}");
            if (!ingredienteResponse.IsSuccessStatusCode) continue;

            var ingredienteJson = await ingredienteResponse.Content.ReadAsStringAsync();
            var ingredienteInfo = JsonSerializer.Deserialize<IngredienteDTO>(ingredienteJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (ingredienteInfo == null) continue;
            var calorias = ingrediente.CantidadGramos * ingredienteInfo.CaloriasPor100g / 100.0; // Calcula las calorías del ingrediente: (gramos * calorías por 100g) / 100
            totalCalorias += calorias; // Suma al total

            // Agrega a la lista para incluir en la respuesta final vvv
            ingredientes.Add(new IngredienteInfo
            {
                Nombre = ingredienteInfo.Nombre,
                CantidadGramos = ingrediente.CantidadGramos,
                CaloriasPor100g = ingredienteInfo.CaloriasPor100g
            });
        }
        
        return new RecetaResultado  // Retorna el resultado con nombre, calorías y lista de ingredientes
        {
            Nombre = receta.Nombre,
            CaloriasTotales = Math.Round(totalCalorias, 2),
            Ingredientes = ingredientes
        };
    }
}
