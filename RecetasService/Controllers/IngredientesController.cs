using Microsoft.AspNetCore.Mvc;
using RecetasService.DTOs;
using RecetasService.Services;

namespace RecetasService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientesController : ControllerBase
{
    private readonly IIngredienteService _service;

    public IngredientesController(IIngredienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IngredienteDTO>>> GetAll()
    {
        var ingredientes = await _service.GetAllAsync();
        return Ok(ingredientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IngredienteDTO>> GetById(int id)
    {
        var ingrediente = await _service.GetByIdAsync(id);
        if (ingrediente == null) return NotFound();
        return Ok(ingrediente);
    }

    [HttpPost]
    public async Task<ActionResult<IngredienteDTO>> Create(IngredienteDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, IngredienteDTO dto)
    {
        try
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
