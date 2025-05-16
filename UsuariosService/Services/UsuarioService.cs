using System.Security.Cryptography;
using System.Text;
using UsuariosService.Models;
using UsuariosService.Repositories;

namespace UsuariosService.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<Usuario?> AutenticarAsync(string email, string password)
    {
        var usuario = await _repository.ObtenerPorEmailAsync(email);
        if (usuario == null) return null;

        var hash = HashPassword(password);
        return usuario.PasswordHash == hash ? usuario : null;
    }

    // listado users
    public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
    {
        return await _repository.ObtenerTodosAsync();
    }


    public async Task<Usuario> RegistrarAsync(string nombre, string email, string password)
    {
        var hash = HashPassword(password);
        var usuario = new Usuario
        {
            Nombre = nombre,
            Email = email,
            PasswordHash = hash
        };

        await _repository.CrearAsync(usuario);
        return usuario;
    }
    
    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        return await _repository.ObtenerPorIdAsync(id);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
