namespace AnalisisService.DTOs;

public class RecetaIngredienteDTO
{
    public string? Nombre { get; set; }
    public int IngredienteId { get; set; }
    public double CantidadGramos { get; set; }
    public double CaloriasPor100g { get; set; }
    
}

public class RecetaDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public List<RecetaIngredienteDTO> Ingredientes { get; set; } = new();
}
