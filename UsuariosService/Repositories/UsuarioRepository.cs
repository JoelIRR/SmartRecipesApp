using Microsoft.EntityFrameworkCore;
using UsuariosService.Data;
using UsuariosService.Models;

namespace UsuariosService.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly UsuariosDbContext _context;

    public UsuarioRepository(UsuariosDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario?> ObtenerPorEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task CrearAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }
}
