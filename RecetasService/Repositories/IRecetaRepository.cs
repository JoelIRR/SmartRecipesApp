using RecetasService.Models;

namespace RecetasService.Repositories;

public interface IRecetaRepository
{
    Task<IEnumerable<Receta>> GetAllAsync();
    Task<Receta?> GetByIdAsync(int id);
    Task<Receta> AddAsync(Receta receta);
    Task UpdateAsync(Receta receta);
    Task DeleteAsync(int id);
}
