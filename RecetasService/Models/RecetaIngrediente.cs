namespace RecetasService.Models;


// Modelo De La Tabla RecetaIngrediente
public class RecetaIngrediente
{
    public int Id { get; set; }

    public int RecetaId { get; set; }
    public Receta Receta { get; set; } = null!;

    public int IngredienteId { get; set; }
    public Ingrediente Ingrediente { get; set; } = null!;

    public double CantidadGramos { get; set; }
}
