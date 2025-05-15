using Microsoft.AspNetCore.Mvc;
using RecetasService.DTOs;
using RecetasService.Services;

namespace RecetasService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecetasController : ControllerBase
{
    private readonly IRecetaService _recetaService;

    public RecetasController(IRecetaService recetaService)
    {
        _recetaService = recetaService;
    }

    // GET: api/recetas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecetaDTO>>> GetAll()
    {
        var recetas = await _recetaService.GetAllAsync();
        return Ok(recetas);
    }

    // GET: api/recetas/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RecetaDTO>> GetById(int id)
    {
        var receta = await _recetaService.GetByIdAsync(id);
        if (receta == null)
            return NotFound();
        return Ok(receta);
    }

    // POST: api/recetas
    [HttpPost]
    public async Task<ActionResult<RecetaDTO>> Create(RecetaDTO dto)
    {
        var created = await _recetaService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/recetas/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RecetaDTO dto)
    {
        try
        {
            await _recetaService.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // DELETE: api/recetas/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _recetaService.DeleteAsync(id);
        return NoContent();
    }
}
