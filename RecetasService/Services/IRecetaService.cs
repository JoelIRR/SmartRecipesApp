using RecetasService.DTOs;

namespace RecetasService.Services;

public interface IRecetaService
{
    Task<IEnumerable<RecetaDTO>> GetAllAsync();
    Task<RecetaDTO?> GetByIdAsync(int id);
    Task<RecetaDTO> CreateAsync(RecetaDTO dto);
    Task UpdateAsync(int id, RecetaDTO dto);
    Task DeleteAsync(int id);
}
