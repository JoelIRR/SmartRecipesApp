using Microsoft.EntityFrameworkCore;
using RecetasService.Data;
using RecetasService.Repositories;
using RecetasService.Services;

// Builder - Contructor
var builder = WebApplication.CreateBuilder(args);

// PostgreSQL connection (detalles en: appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// PostgreSQL connection

// Servicios y Repositorios
builder.Services.AddScoped<IRecetaRepository, RecetaRepository>();
builder.Services.AddScoped<IRecetaService, RecetaService>();
builder.Services.AddScoped<IIngredienteRepository, IngredienteRepository>();
builder.Services.AddScoped<IIngredienteService, IngredienteService>();
// Servicios y Repositorios


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build con los servicios  /\
//                          ||
//                          ||
var app = builder.Build();

// Migraciones automaticas al iniciar
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}
// Migraciones automaticas al iniciar

// interfaz gráfica.
app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

//Mapeo de rutas
app.MapControllers();

app.Run();
