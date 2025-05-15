namespace RecetasService.DTOs;

public class IngredienteDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public double CaloriasPor100g { get; set; }
}
