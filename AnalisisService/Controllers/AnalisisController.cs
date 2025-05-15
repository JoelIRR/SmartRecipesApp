using Microsoft.AspNetCore.Mvc;
using AnalisisService.Services;

namespace AnalisisService.Controllers;

[ApiController]
[Route("api/[controller]")]// La ruta será: api/analisisservice
public class AnalisisServiceController : ControllerBase //El controlador llama a la logica del servicio por medio de la instancia
{
    private readonly IAnalisisService _service;

    public AnalisisServiceController(IAnalisisService service) // El controlador usará esta instancia para llamar a la lógica del servicio
    {
        _service = service;
    }

    // Llama al servicio para obtener los datos de la receta con sus calorías
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarReceta(int id)
    {
        var resultado = await _service.ObtenerRecetaConCaloriasAsync(id);
        if (resultado == null) return NotFound();

        return Ok(resultado);
    }
}
