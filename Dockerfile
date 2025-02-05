# Använd officiell .NET SDK-avbildning för build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Kopiera csproj och återställ beroenden
COPY *.csproj .
RUN dotnet restore

# Kopiera återstående filer
COPY . .
RUN dotnet publish -c release -o /app

# Skapa körbar avbildning
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Portfolio.dll"]
