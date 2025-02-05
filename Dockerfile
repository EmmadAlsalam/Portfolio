# Använd officiell .NET SDK-avbildning
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Kopiera csproj och återställ beroenden
COPY *.csproj ./
RUN dotnet restore

# Kopiera återstående filer och bygg
COPY . ./
RUN dotnet publish -c Release -o out

# Skapa körbar avbildning
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Portfolio.dll"]
