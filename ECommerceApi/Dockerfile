FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# نسخ ملفات الإعدادات أولاً لتسريع البناء
COPY ["ECommerceApi/ECommerceApi.csproj", "ECommerceApi/"]
COPY ["ECommerceApi.Application/ECommerceApi.Application.csproj", "ECommerceApi.Application/"]
COPY ["ECommerceApi.Domain/ECommerceApi.Domain.csproj", "ECommerceApi.Domain/"]
COPY ["ECommerceApi.Infrastructure/ECommerceApi.Infrastructure.csproj", "ECommerceApi.Infrastructure/"]

RUN dotnet restore "ECommerceApi/ECommerceApi.csproj"

# نسخ باقي الكود وبناء المشروع
COPY . .
WORKDIR "/src/ECommerceApi"
RUN dotnet build "ECommerceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerceApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# النسخة الخفيفة لتشغيل الـ API
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceApi.dll"]