# Use the official .NET 8.0 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the official .NET 8.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/CCRS.Api/CCRS.Api.csproj", "src/CCRS.Api/"]
COPY ["src/CCRS.Business/CCRS.Business.csproj", "src/CCRS.Business/"]
COPY ["src/CCRS.Data/CCRS.Data.csproj", "src/CCRS.Data/"]

RUN dotnet restore "src/CCRS.Api/CCRS.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/src/CCRS.Api"
RUN dotnet build "CCRS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CCRS.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create a non-root user
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

ENTRYPOINT ["dotnet", "CCRS.Api.dll"]