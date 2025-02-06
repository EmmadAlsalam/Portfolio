# Använd officiell .NET SDK-avbildning för build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Kopiera projektfiler och återställ beroenden
COPY *.csproj .
RUN dotnet restore

# Kopiera återstående filer och bygg
COPY . .
RUN dotnet publish -c release -o /app

# Skapa körbar avbildning
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Ange miljövariabler
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

# Exponera port
EXPOSE 8080

ENTRYPOINT ["dotnet", "Portfolio.dll"]
