# Dockerfile

# Base image voor .NET 6.0
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
# Map aanmaken
WORKDIR /app

# Kopieren van alle code van de root map naar de container
COPY Oogarts.csproj ./Oogarts.csproj
COPY src/Client/Client.csproj ./src/Client/Client.csproj
COPY src/Domain/Domain.csproj ./src/Domain/Domain.csproj
COPY src/Fakers/Fakers.csproj ./src/Fakers/Fakers.csproj
COPY src/Persistence/Persistence.csproj ./src/Persistence/Persistence.csproj
COPY src/Server/Server.csproj ./src/Server/Server.csproj
COPY src/Services/Services.csproj ./src/Services/Services.csproj
COPY src/Shared/Shared.csproj ./src/Shared/Shared.csproj

# Restore van de packages
RUN dotnet restore Oogarts.csproj
RUN dotnet restore src/Client/Client.csproj
RUN dotnet restore src/Domain/Domain.csproj
RUN dotnet restore src/Fakers/Fakers.csproj
RUN dotnet restore src/Persistence/Persistence.csproj
RUN dotnet restore src/Server/Server.csproj
RUN dotnet restore src/Services/Services.csproj
RUN dotnet restore src/Shared/Shared.csproj

# Kopieren van de rest van de applicatie naar de container
COPY . ./
# Builden van de applicatie
RUN dotnet publish -c Release -o publish

# Maken van een final build image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final-env
# Map aanmaken
WORKDIR /app
EXPOSE 80
# Kopieren van de vorige container naar de nieuwe container
COPY --from=build-env /app/publish .

# App starten
ENTRYPOINT ["dotnet", "Server.dll"]
