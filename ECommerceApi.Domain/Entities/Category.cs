using ECommerceApi.Domain.Common;
using ECommerceApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }

        private readonly List<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        private Category() { } 

        public Category(string name) // public constructor for creating a new category
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Category name cannot be empty.");

            Name = name;
        }
    }
}
