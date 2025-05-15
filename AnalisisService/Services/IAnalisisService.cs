using AnalisisService.Models;

namespace AnalisisService.Services;

public interface IAnalisisService   //define el contrato del servicio de análisis
{
    Task<RecetaResultado?> ObtenerRecetaConCaloriasAsync(int recetaId);
}
