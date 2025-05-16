using Microsoft.EntityFrameworkCore;
using UsuariosService.Data;
using UsuariosService.Models;



namespace UsuariosService.Repositories;

public class RecetaFavoritaRepository : IRecetaFavoritaRepository
{
    private readonly UsuariosDbContext _context;

    public RecetaFavoritaRepository(UsuariosDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<int>> ObtenerRecetasFavoritasAsync(int usuarioId)
    {
        return await _context.RecetasFavoritas
            .Where(rf => rf.UsuarioId == usuarioId)
            .Select(rf => rf.RecetaId)
            .ToListAsync();
    }

    public async Task AgregarFavoritaAsync(int usuarioId, int recetaId)
    {
        var existe = await _context.RecetasFavoritas
            .AnyAsync(rf => rf.UsuarioId == usuarioId && rf.RecetaId == recetaId);

        if (!existe)
        {
            _context.RecetasFavoritas.Add(new RecetaFavorita
            {
                UsuarioId = usuarioId,
                RecetaId = recetaId
            });

            await _context.SaveChangesAsync();
        }
    }

    public async Task EliminarFavoritaAsync(int usuarioId, int recetaId)
    {
        var favorita = await _context.RecetasFavoritas
            .FirstOrDefaultAsync(rf => rf.UsuarioId == usuarioId && rf.RecetaId == recetaId);

        if (favorita != null)
        {
            _context.RecetasFavoritas.Remove(favorita);
            await _context.SaveChangesAsync();
        }
    }
}
