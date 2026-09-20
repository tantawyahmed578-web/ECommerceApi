// Domain/Entities/Customer.cs — عدّل السطور دي
using ECommerceApi.Domain.Common;
using ECommerceApi.Domain.Enums;
using ECommerceApi.Domain.Exceptions;

namespace ECommerceApi.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; } = UserRole.Customer;

    private Customer() { }

    public Customer(string name, string email, string passwordHash, UserRole role = UserRole.Customer)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Customer name cannot be empty.");
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new DomainException("A valid email is required.");

        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }
}