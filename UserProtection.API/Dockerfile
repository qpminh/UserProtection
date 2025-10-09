# ===================== BUILD STAGE =====================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy tất cả csproj theo thứ tự dependency
COPY ["UserProtection.API/UserProtection.API.csproj", "UserProtection.API/"]
COPY ["UserProtection.Application/UserProtection.Application.csproj", "UserProtection.Application/"]
COPY ["UserProtection.Infrastructure/UserProtection.Infrastructure.csproj", "UserProtection.Infrastructure/"]
COPY ["UserProtection.Domain/UserProtection.Domain.csproj", "UserProtection.Domain/"]

# Khôi phục dependency (restore)
RUN dotnet restore "UserProtection.API/UserProtection.API.csproj"

# Copy toàn bộ source code
COPY . .

# Build và publish ra thư mục /app/publish
WORKDIR "/src/UserProtection.API"
RUN dotnet publish "UserProtection.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===================== RUNTIME STAGE =====================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Render sẽ map port động → đặt biến môi trường để app dùng cổng Render cung cấp
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080}

EXPOSE 8080
ENTRYPOINT ["dotnet", "UserProtection.API.dll"]
