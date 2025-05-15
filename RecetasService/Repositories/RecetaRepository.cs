using Microsoft.EntityFrameworkCore;
using RecetasService.Data;
using RecetasService.Models;


// Este repositorio no transforma los datos a DTOs, ni aplica reglas de negocio, solo trabaja con las entidades Receta
namespace RecetasService.Repositories;

public class RecetaRepository : IRecetaRepository
{
    private readonly ApplicationDbContext _context;

    public RecetaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Receta>> GetAllAsync()
    {
        return await _context.Recetas
            .Include(r => r.RecetaIngredientes)
            .ThenInclude(ri => ri.Ingrediente)
            .ToListAsync();
    }

    public async Task<Receta?> GetByIdAsync(int id)
    {
        return await _context.Recetas
            .Include(r => r.RecetaIngredientes)
            .ThenInclude(ri => ri.Ingrediente)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Receta> AddAsync(Receta receta)
    {
        _context.Recetas.Add(receta);
        await _context.SaveChangesAsync();
        return receta;
    }

    public async Task UpdateAsync(Receta receta)
    {
        _context.Recetas.Update(receta);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var receta = await _context.Recetas.FindAsync(id);
        if (receta is not null)
        {
            _context.Recetas.Remove(receta);
            await _context.SaveChangesAsync();
        }
    }
}
