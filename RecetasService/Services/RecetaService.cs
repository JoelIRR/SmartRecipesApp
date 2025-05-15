using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecetasService.Data;
using RecetasService.DTOs;
using RecetasService.Models;
using RecetasService.Repositories;


// Aplicar reglas de negocio, transformar entidades en DTOs y usar el repositorio para obtener o modificar los datos.
namespace RecetasService.Services;

public class RecetaService : IRecetaService
{
    private readonly IRecetaRepository _repo;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public RecetaService(IRecetaRepository repo, ApplicationDbContext context, IMapper mapper)
    {
        _repo = repo;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecetaDTO>> GetAllAsync()
    {
        var recetas = await _repo.GetAllAsync();
        return recetas.Select(r => new RecetaDTO
        {
            Id = r.Id,
            Nombre = r.Nombre,
            Descripcion = r.Descripcion,
            Ingredientes = r.RecetaIngredientes.Select(ri => new RecetaIngredienteDTO
            {
                IngredienteId = ri.IngredienteId,
                CantidadGramos = ri.CantidadGramos
            }).ToList()
        });
    }

    public async Task<RecetaDTO?> GetByIdAsync(int id)
    {
        var receta = await _repo.GetByIdAsync(id);
        if (receta is null) return null;

        return new RecetaDTO
        {
            Id = receta.Id,
            Nombre = receta.Nombre,
            Descripcion = receta.Descripcion,
            Ingredientes = receta.RecetaIngredientes.Select(ri => new RecetaIngredienteDTO
            {
                IngredienteId = ri.IngredienteId,
                CantidadGramos = ri.CantidadGramos
            }).ToList()
        };
    }

    public async Task<RecetaDTO> CreateAsync(RecetaDTO dto)
    {
        var receta = new Receta
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            RecetaIngredientes = dto.Ingredientes.Select(ri => new RecetaIngrediente
            {
                IngredienteId = ri.IngredienteId,
                CantidadGramos = ri.CantidadGramos
            }).ToList()
        };

        var result = await _repo.AddAsync(receta);

        dto.Id = result.Id;
        return dto;
    }

    public async Task UpdateAsync(int id, RecetaDTO dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) throw new Exception("Receta no encontrada");

        // Eliminar los ingredientes anteriores y agregar los nuevos
        _context.RecetaIngredientes.RemoveRange(existing.RecetaIngredientes);

        existing.Nombre = dto.Nombre;
        existing.Descripcion = dto.Descripcion;
        existing.RecetaIngredientes = dto.Ingredientes.Select(ri => new RecetaIngrediente
        {
            IngredienteId = ri.IngredienteId,
            CantidadGramos = ri.CantidadGramos
        }).ToList();

        await _repo.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }
}
