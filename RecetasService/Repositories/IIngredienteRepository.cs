using RecetasService.Models;

namespace RecetasService.Repositories;

// Define el contrato que cualquier servicio de lógica de negocio para ingredientes debe cumplir. Trabaja con DTOs(IngredienteDTO) porque expone los datos que se devuelven/controlan desde la API.
public interface IIngredienteRepository
{
    Task<IEnumerable<Ingrediente>> GetAllAsync();
    Task<Ingrediente?> GetByIdAsync(int id);
    Task<Ingrediente> AddAsync(Ingrediente ingrediente);
    Task UpdateAsync(Ingrediente ingrediente);
    Task DeleteAsync(int id);
}
