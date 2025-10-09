# ===================== BUILD STAGE =====================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy tất cả csproj theo thứ tự dependency
COPY ["UserProtection.API/UserProtection.API.csproj", "UserProtection.API/"]
COPY ["UserProtection.Application/UserProtection.Application.csproj", "UserProtection.Application/"]
COPY ["UserProtection.Infrastructure/UserProtection.Infrastructure.csproj", "UserProtection.Infrastructure/"]
COPY ["UserProtection.Domain/UserProtection.Domain.csproj", "UserProtection.Domain/"]

# Restore dependency
RUN dotnet restore "UserProtection.API/UserProtection.API.csproj"

# Copy toàn bộ source code
COPY . .

# Build và publish ra thư mục /app/publish
WORKDIR "/src/UserProtection.API"
RUN dotnet publish "UserProtection.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===================== RUNTIME STAGE =====================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy publish output từ build stage
COPY --from=build /app/publish .

# Render sẽ cung cấp biến PORT, nhưng ta cần fallback 8080 để Docker biết port tại build-time
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080}
EXPOSE 8080

ENTRYPOINT ["dotnet", "UserProtection.API.dll"]
