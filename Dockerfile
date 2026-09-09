FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ECommerceApi.Domain/ECommerceApi.Domain.csproj", "ECommerceApi.Domain/"]
COPY ["ECommerceApi.Application/ECommerceApi.Application.csproj", "ECommerceApi.Application/"]
COPY ["ECommerceApi.Infrastructure/ECommerceApi.Infrastructure.csproj", "ECommerceApi.Infrastructure/"]
COPY ["ECommerceApi/ECommerceApi.csproj", "ECommerceApi/"]
RUN dotnet restore "ECommerceApi/ECommerceApi.csproj"

COPY . .
WORKDIR "/src/ECommerceApi"
RUN dotnet build "ECommerceApi.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "ECommerceApi.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD dotnet --version || exit 1
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceApi.dll"]
