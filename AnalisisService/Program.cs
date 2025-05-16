using AnalisisService.Services;
using Microsoft.OpenApi.Models;

// Builder - Contructor
var builder = WebApplication.CreateBuilder(args);

// Registra los controladores como servicios, necesarios para que los endpoints de tipo [ApiController] funcionen
builder.Services.AddControllers();


// Service de HealtChecks 
builder.Services.AddHealthChecks();

// Configura un HttpClient para inyectarse cuando se solicite IAnalisisService.
// Este cliente usará como base la URL interna del contenedor de RecetasService, permitiendo la comunicación entre microservicios en docker
builder.Services.AddHttpClient<IAnalisisService, AnalisisService.Services.AnalisisService>(client =>
{
    client.BaseAddress = new Uri("http://recetasapi:8080"); // comunicación interna en Docker
});


// Swagger para probar Endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Analisis API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Analisis API v1");
    });
}
// Endpoint de health check
app.MapHealthChecks("/health");
// Mapea rutas HTTP
app.MapControllers();

app.Run();