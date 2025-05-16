using Microsoft.EntityFrameworkCore;
using UsuariosService.Data;
using UsuariosService.Repositories;
using UsuariosService.Services;

var builder = WebApplication.CreateBuilder(args);

// Service de HealtChecks 
builder.Services.AddHealthChecks();

// Configurar DbContext con cadena de conexión
builder.Services.AddDbContext<UsuariosDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias
builder.Services.AddScoped<IRecetaFavoritaService, RecetaFavoritaService>();
builder.Services.AddScoped<IRecetaFavoritaRepository, RecetaFavoritaRepository>();

builder.Services.AddHttpClient<IRecetaProxyService, RecetaProxyService>(client =>
{
    client.BaseAddress = new Uri("http://recetasapi:8080");
});

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Configuración general
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Migraciones automáticas al iniciar
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UsuariosDbContext>();
    dbContext.Database.Migrate();
}

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoint de health check
app.MapHealthChecks("/health");
app.UseAuthorization();
app.MapControllers();

app.Run();
