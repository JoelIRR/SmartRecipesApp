FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["AnalisisService.csproj", "./"]
RUN dotnet restore "./AnalisisService.csproj"

COPY . .
RUN dotnet publish "AnalisisService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app



COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AnalisisService.dll"]
