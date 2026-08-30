using ECommerceApi.Domain.Common;
using ECommerceApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        private Customer() { }

        public Customer(string name, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Customer name cannot be empty."); // Validate that the name is not null or whitespace
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new DomainException("A valid email is required."); // Validate that the email is not null or whitespace and contains '@'

            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
