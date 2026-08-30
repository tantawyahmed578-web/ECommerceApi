using ECommerceApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto); // Register a new customer
        Task<AuthResponseDto> LoginAsync(LoginDto dto); // Authenticate a customer
    }
}
