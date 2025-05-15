# Imagen base para el runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Imagen para el build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiamos el proyecto y restauramos
COPY ["ApiGateway.csproj", "./"]
RUN dotnet restore "ApiGateway.csproj"

# Copiamos todo el contenido y publicamos
COPY . .
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app

# Copiamos el build publicado
COPY --from=build /app/publish .

# Expone el puerto 80 para el gateway
ENV ASPNETCORE_URLS=http://+:80

# Inicia la app
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
