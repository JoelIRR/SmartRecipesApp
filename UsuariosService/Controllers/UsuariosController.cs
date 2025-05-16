using Microsoft.AspNetCore.Mvc;
using UsuariosService.Models;
using UsuariosService.Services;
using UsuariosService.DTOs;

namespace UsuariosService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IRecetaFavoritaService _recetaFavoritaService;
    private readonly IRecetaProxyService _recetaProxyService;

    public UsuariosController(
        IUsuarioService usuarioService,
        IRecetaFavoritaService recetaFavoritaService,
        IRecetaProxyService recetaProxyService)
    {
        _usuarioService = usuarioService;
        _recetaFavoritaService = recetaFavoritaService;
        _recetaProxyService = recetaProxyService;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDTO dto)
    {
        var usuario = await _usuarioService.RegistrarAsync(dto.Nombre, dto.Email, dto.Password);
        return Ok(new { usuario.Id, usuario.Nombre, usuario.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
    {
        var usuario = await _usuarioService.AutenticarAsync(dto.Email, dto.Password);
        if (usuario == null) return Unauthorized(new { mensaje = "Credenciales inválidas" });

        return Ok(new { usuario.Id, usuario.Nombre, usuario.Email });
    }

    //Agregar receta favorita
    [HttpPost("{usuarioId}/favoritas/{recetaId}")]
    public async Task<IActionResult> AgregarFavorita(int usuarioId, int recetaId)
    {
        var recetaExiste = await _recetaProxyService.RecetaExisteAsync(recetaId);
        if (!recetaExiste)
        {
            return NotFound(new { mensaje = "La receta no existe en el servicio de recetas." });
        }

        var favorita = await _recetaFavoritaService.AgregarFavoritaAsync(usuarioId, recetaId);
        return Ok(favorita);
    }

    //Obtener favoritas de un usuario
    [HttpGet("{usuarioId}/favoritas")]
    public async Task<IActionResult> ObtenerFavoritas(int usuarioId)
    {
        var favoritas = await _recetaFavoritaService.ObtenerFavoritasPorUsuarioAsync(usuarioId);
        return Ok(favoritas);
    }

    //Eliminar favorita
    [HttpDelete("{usuarioId}/favoritas/{recetaId}")]
    public async Task<IActionResult> EliminarFavorita(int usuarioId, int recetaId)
    {
        await _recetaFavoritaService.EliminarFavoritaAsync(usuarioId, recetaId);
        return NoContent();
    }

    //Lista de todos los usuarios y sus correos (id tmb)
    [HttpGet("listado")]
    public async Task<IActionResult> ListarUsuarios()
    {
        var usuarios = await _usuarioService.ObtenerTodosAsync();
        var resultado = usuarios.Select(u => new {
            u.Id,
            u.Nombre,
            u.Email
        });

        return Ok(resultado);
    }


    //Nombre de usuario y listado de recetas favoritas
    [HttpGet("{usuarioId}/favoritas/detalle")]
    public async Task<IActionResult> ObtenerFavoritasConDetalle(int usuarioId)
    {
        var usuario = await _usuarioService.ObtenerPorIdAsync(usuarioId);
        if (usuario == null)
        {
            return NotFound(new { mensaje = "Usuario no encontrado." });
        }

        var favoritas = await _recetaFavoritaService.ObtenerFavoritasPorUsuarioAsync(usuarioId);
        var recetaIds = favoritas.Select(f => f.RecetaId).ToList();

        var nombresRecetas = new List<string>();
        foreach (var id in recetaIds)
        {
            var nombre = await _recetaProxyService.ObtenerNombreRecetaAsync(id);
            if (!string.IsNullOrEmpty(nombre))
            {
                nombresRecetas.Add(nombre);
            }
        }

        var resultado = new UsuarioFavoritasDTO
        {
            NombreUsuario = usuario.Nombre,
            RecetasFavoritas = nombresRecetas
        };

        return Ok(resultado);
    }    


}
