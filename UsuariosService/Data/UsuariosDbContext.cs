using Microsoft.EntityFrameworkCore;
using UsuariosService.Models;

namespace UsuariosService.Data;

public class UsuariosDbContext : DbContext
{
    public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<RecetaFavorita> RecetasFavoritas => Set<RecetaFavorita>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RecetaFavorita>()
            .HasKey(rf => rf.Id);

        modelBuilder.Entity<RecetaFavorita>()
            .HasOne(rf => rf.Usuario)
            .WithMany(u => u.RecetasFavoritas)
            .HasForeignKey(rf => rf.UsuarioId);
    }
}
