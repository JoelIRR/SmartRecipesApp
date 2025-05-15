namespace RecetasService.Models;


// Modelo De La Tabla Ingrediente
public class Ingrediente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public double CaloriasPor100g { get; set; }

    public ICollection<RecetaIngrediente> RecetaIngredientes { get; set; } = new List<RecetaIngrediente>();
}
