# Etap build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiujemy projekty
COPY ./shared_models/ ./shared_models/

# Jeśli masz solution file w root folderze
COPY ./offer_manager.sln ./offer_manager.sln

WORKDIR /src/offer_manager

# Restore całego solution
RUN dotnet restore ../offer_manager.sln

# Publish tylko tego projektu
RUN dotnet publish offer_manager.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "offer_manager.dll"]
