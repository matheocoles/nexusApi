# Étape de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier le fichier projet et restaurer les dépendances
COPY ["NexusAPI/NexusAPI.csproj", "NexusAPI/"]
RUN dotnet restore "NexusAPI/NexusAPI.csproj"

# Copier tout le reste et builder
COPY . .
WORKDIR "/src/NexusAPI"
RUN dotnet publish "NexusAPI.csproj" -c Release -o /app/publish

# Étape finale
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Railway utilise souvent le port 8080 par défaut
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "NexusAPI.dll"]
