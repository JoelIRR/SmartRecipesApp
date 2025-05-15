using RecetasService.DTOs;

namespace RecetasService.Services;


/// Define las operaciones que cualquier repositorio de ingredientes debe ofrecer. Solo usa entidades reales del dominio (ingrediente), no con DTOs.
public interface IIngredienteService
{
    Task<IEnumerable<IngredienteDTO>> GetAllAsync();
    Task<IngredienteDTO?> GetByIdAsync(int id);
    Task<IngredienteDTO> CreateAsync(IngredienteDTO dto);
    Task UpdateAsync(int id, IngredienteDTO dto);
    Task DeleteAsync(int id);
}
