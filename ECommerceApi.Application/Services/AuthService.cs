using AutoMapper;
using ECommerceApi.Application.DTOs;
using ECommerceApi.Application.Interfaces;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Exceptions;
using ECommerceApi.Domain.Interfaces;

namespace ECommerceApi.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public AuthService(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var existing = await _unitOfWork.Customers.GetByEmailAsync(dto.Email);
        if (existing is not null)
            throw new DomainException("A customer with this email already exists.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var customer = new Customer(dto.Name, dto.Email, passwordHash);

        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();

        var token = _tokenService.GenerateToken(customer);
        return new AuthResponseDto { Token = token, Customer = _mapper.Map<CustomerDto>(customer) };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var customer = await _unitOfWork.Customers.GetByEmailAsync(dto.Email)
            ?? throw new DomainException("Invalid email or password.");

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, customer.PasswordHash);
        if (!isPasswordValid)
            throw new DomainException("Invalid email or password.");

        var token = _tokenService.GenerateToken(customer);
        return new AuthResponseDto { Token = token, Customer = _mapper.Map<CustomerDto>(customer) };
    }
}