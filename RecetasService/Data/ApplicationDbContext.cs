using Microsoft.EntityFrameworkCore;
using RecetasService.Models;

namespace RecetasService.Data;

// ||ApplicationDbContext : DbContext|| --> Esta clase hereda de DbContext, el núcleo de EF Core. Se encarga de representar la base de datos y exponer las tablas como propiedades.
public class ApplicationDbContext : DbContext
{   
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) // Opciones de configuración desde Program.cs
        : base(options) { }

    /// <DbSet>
    /// Cada DbSet<T> representa una tabla en la base de datos.
    /// EF Core usa estos DbSet para ejecutar queries, insertar datos, etc.
    /// </DbSet>
    public DbSet<Receta> Recetas => Set<Receta>();
    public DbSet<Ingrediente> Ingredientes => Set<Ingrediente>();
    public DbSet<RecetaIngrediente> RecetaIngredientes => Set<RecetaIngrediente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)  // definiciones de relaciones, claves foráneas, restricciones, etc.
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RecetaIngrediente>()
            .HasOne(ri => ri.Receta)
            .WithMany(r => r.RecetaIngredientes)
            .HasForeignKey(ri => ri.RecetaId);

        modelBuilder.Entity<RecetaIngrediente>()
            .HasOne(ri => ri.Ingrediente)
            .WithMany(i => i.RecetaIngredientes)
            .HasForeignKey(ri => ri.IngredienteId);
    }
}


//  Una RecetaIngrediente tiene una Receta y un Ingrediente.
//  una Receta puede tener muchos RecetaIngredientes.
//  un Ingrediente puede estar en muchos RecetaIngredientes.
//  definiciones claves foráneas (RecetaId, IngredienteId).
