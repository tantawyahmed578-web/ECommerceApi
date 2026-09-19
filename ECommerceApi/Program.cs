using ECommerceApi.Application.Interfaces;
using ECommerceApi.Application.Mappings;
using ECommerceApi.Application.Services;
using ECommerceApi.Domain.Interfaces;
using ECommerceApi.Infrastructure.Persistence;
using ECommerceApi.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using System.Text;
using ECommerceApi.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using StackExchange.Redis;
using ECommerceApi.Middlewares;
using Microsoft.EntityFrameworkCore;

Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// --- Database (Day 3) ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Unit of Work (Day 5) ---
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// --- AutoMapper (Day 6) ---
builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

// --- Application services (Day 6) ---
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// 1. تسجيل اتصال Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisConfig = builder.Configuration.GetConnectionString("Redis")
                      ?? throw new InvalidOperationException("Redis connection string is missing.");
    var configuration = ConfigurationOptions.Parse(redisConfig, true);
    return ConnectionMultiplexer.Connect(configuration);
});

// 2. تسجيل مستودع الـ Basket
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// --- MVC / Controllers ---
builder.Services.AddControllers();

// --- Swagger (Day 7) ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Paste ONLY the raw token here — no need to type Bearer, Swagger adds it for you."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "حدث خطأ أثناء إنشاء قاعدة البيانات.");
    }
}

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

// --- Global Exception Handling Middleware ---
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (ECommerceApi.Domain.Exceptions.DomainException ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogWarning(ex, "A business rule was violated.");

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            statusCode = StatusCodes.Status400BadRequest,
            message = ex.Message
        });
    }
    catch (UnauthorizedAccessException ex)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { statusCode = 401, message = ex.Message });
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An unhandled exception occurred while processing the request.");

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        var response = new
        {
            statusCode = StatusCodes.Status500InternalServerError,
            message = app.Environment.IsDevelopment()
                ? $"An error occurred: {ex.Message}\n{ex.StackTrace}"
                : "An unhandled exception occurred while processing the request.",
            details = app.Environment.IsDevelopment() ? ex.InnerException?.Message : null
        };
        await context.Response.WriteAsJsonAsync(response);
    }
});

// --- Middleware pipeline ---

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API v1"));


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();