using RecetasService.DTOs;
using RecetasService.Models;
using RecetasService.Repositories;

//  Encapsula la lógica de negocio y trabaja con los DTOs
namespace RecetasService.Services;

public class IngredienteService : IIngredienteService
{
    private readonly IIngredienteRepository _repo;

    public IngredienteService(IIngredienteRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<IngredienteDTO>> GetAllAsync()  // Pide los ingredientes al repositorio y convierte cada Ingrediente(modelo) en un IngredienteDTO
    {
        var ingredientes = await _repo.GetAllAsync();
        return ingredientes.Select(i => new IngredienteDTO
        {
            Id = i.Id,
            Nombre = i.Nombre,
            CaloriasPor100g = i.CaloriasPor100g
        });
    }

    public async Task<IngredienteDTO?> GetByIdAsync(int id)        // Busca el ingrediente en la base de datos.  Lo convierte a DTO -> SI ES ENCONTRADO
    {
        var ingrediente = await _repo.GetByIdAsync(id);
        if (ingrediente == null) return null;

        return new IngredienteDTO
        {
            Id = ingrediente.Id,
            Nombre = ingrediente.Nombre,
            CaloriasPor100g = ingrediente.CaloriasPor100g
        };
    }

    public async Task<IngredienteDTO> CreateAsync(IngredienteDTO dto) // Convierte el DTO en una entidad(Ingrediente).  Lo guarda en la base de datos usando el repositorioy regresa el DTO con el id actualizado
    {
        var ingrediente = new Ingrediente
        {
            Nombre = dto.Nombre,
            CaloriasPor100g = dto.CaloriasPor100g
        };

        var created = await _repo.AddAsync(ingrediente);
        dto.Id = created.Id;
        return dto;
    }

    public async Task UpdateAsync(int id, IngredienteDTO dto)          // Busca el ingrediente.  Actualiza sus propiedades.  Lo guarda nuevamente
    {
        var ingrediente = await _repo.GetByIdAsync(id);
        if (ingrediente == null) throw new Exception("Ingrediente no encontrado");

        ingrediente.Nombre = dto.Nombre;
        ingrediente.CaloriasPor100g = dto.CaloriasPor100g;
        await _repo.UpdateAsync(ingrediente);
    }

    public async Task DeleteAsync(int id)                       // Llama al repositorio para borrar el ingrediente por ID
    {
        await _repo.DeleteAsync(id);
    }
}
