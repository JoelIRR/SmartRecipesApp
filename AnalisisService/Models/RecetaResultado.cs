namespace AnalisisService.Models;

public class RecetaResultado
{
    public string Nombre { get; set; } = null!;
    public double CaloriasTotales { get; set; }
    public List<IngredienteInfo> Ingredientes { get; set; } = new();
}

public class IngredienteInfo
{
    public string Nombre { get; set; } = null!;
    public double CantidadGramos { get; set; }
    public double CaloriasPor100g { get; set; }
}
