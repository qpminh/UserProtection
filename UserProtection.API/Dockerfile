# ===================== BUILD STAGE =====================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["UserProtection.API/UserProtection.API.csproj", "UserProtection.API/"]
COPY ["UserProtection.Application/UserProtection.Application.csproj", "UserProtection.Application/"]
COPY ["UserProtection.Infrastructure/UserProtection.Infrastructure.csproj", "UserProtection.Infrastructure/"]
COPY ["UserProtection.Domain/UserProtection.Domain.csproj", "UserProtection.Domain/"]

RUN dotnet restore "UserProtection.API/UserProtection.API.csproj"

COPY . .
WORKDIR "/src/UserProtection.API"
RUN dotnet publish "UserProtection.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===================== RUNTIME STAGE =====================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Render cung cấp biến PORT, fallback 8080 nếu không có
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080}
EXPOSE 8080

ENTRYPOINT ["dotnet", "UserProtection.API.dll"]
