using Microsoft.AspNetCore.Mvc;
using UsuariosService.Models;
using UsuariosService.DTOs;
using UsuariosService.Services;
using UsuariosService.Repositories;



namespace UsuariosService.Controllers;


[ApiController]
[Route("api/[controller]")]
public class FavoritasController : ControllerBase
{
    private readonly IRecetaFavoritaRepository _repository;

    public FavoritasController(IRecetaFavoritaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> ObtenerFavoritas(int usuarioId)
    {
        var recetas = await _repository.ObtenerRecetasFavoritasAsync(usuarioId);
        return Ok(recetas);
    }

    [HttpPost("{usuarioId}/{recetaId}")]
    public async Task<IActionResult> AgregarFavorita(int usuarioId, int recetaId)
    {
        await _repository.AgregarFavoritaAsync(usuarioId, recetaId);
        return Ok(new { mensaje = "Receta favorita agregada" });
    }

    [HttpDelete("{usuarioId}/{recetaId}")]
    public async Task<IActionResult> EliminarFavorita(int usuarioId, int recetaId)
    {
        await _repository.EliminarFavoritaAsync(usuarioId, recetaId);
        return Ok(new { mensaje = "Receta favorita eliminada" });
    }
}
