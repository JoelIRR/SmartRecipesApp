using Microsoft.EntityFrameworkCore;
using RecetasService.Data;
using RecetasService.Models;


// Conectarse con la base de datos a través del DbContext
namespace RecetasService.Repositories;

public class IngredienteRepository : IIngredienteRepository
{
    private readonly ApplicationDbContext _context;

    public IngredienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Ingrediente>> GetAllAsync()   // Consulta todos los ingredientes en la tabla
    {
        return await _context.Ingredientes.ToListAsync();
    }

    public async Task<Ingrediente?> GetByIdAsync(int id)        // Busca un ingrediente por id.
    {
        return await _context.Ingredientes.FindAsync(id);
    }

    public async Task<Ingrediente> AddAsync(Ingrediente ingrediente) // Agrega un nuevo ingrediente y guarda los cambios.
    {
        _context.Ingredientes.Add(ingrediente);
        await _context.SaveChangesAsync();
        return ingrediente;
    }

    public async Task UpdateAsync(Ingrediente ingrediente)      // Actualiza los datos de un ingrediente ya existente.
    {
        _context.Ingredientes.Update(ingrediente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)                       // Elimina un ingrediente si existe. (pide el {id} del ingrediente)
    {
        var ingrediente = await _context.Ingredientes.FindAsync(id);
        if (ingrediente != null)
        {
            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();
        }
    }
}
