
// Builder - Contructor
var builder = WebApplication.CreateBuilder(args);

// Registra el servicio de Reverse Proxy (YARP) en el contenedor de dependencias
// .LoadFromConfig(...): Indica que las rutas y destinos del proxy se van a cargar desde el archivo appsettings.json, específicamente de la sección "ReverseProxy"
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Indica que esta aplicación maneja solicitudes como un proxy inverso.Redirige automáticamente las peticiones entrantes según lo que esté definido en el "appsettings.json"
app.MapReverseProxy();

app.Run();
