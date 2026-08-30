// Infrastructure/Services/TokenService.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerceApi.Application.Interfaces;
using ECommerceApi.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceApi.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Customer customer)
    { 
        var jwtSettings = _configuration.GetSection("Jwt"); // Get the "Jwt" section from the configuration
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>  
        {
            new(JwtRegisteredClaimNames.Sub, customer.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, customer.Email),
            new(ClaimTypes.Name, customer.Name),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"]!)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token); 
    }
}