using ECommerceApi.Domain.Common;
using ECommerceApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; } = null!;

        private Product() { }

        public Product(string name, decimal price, int stockQuantity, int categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty."); // Validate that the name is not null or whitespace
            if (price <= 0)
                throw new DomainException("Product price must be greater than zero."); // Validate that the price is greater than zero
            if (stockQuantity < 0)
                throw new DomainException("Stock quantity cannot be negative."); // Validate that the stock quantity is not negative

            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
            CategoryId = categoryId;
        }

        public void DecreaseStock(int quantity) // Method to decrease stock quantity
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive.");
            if (quantity > StockQuantity)
                throw new InsufficientStockException(Name, StockQuantity, quantity);

            StockQuantity -= quantity;
        }

        public void IncreaseStock(int quantity) // Method to increase stock quantity
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive.");

            StockQuantity += quantity;
        }

        public void UpdateDetails(string name, decimal price) // Method to update product details
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty.");
            if (price <= 0)
                throw new DomainException("Product price must be greater than zero.");

            Name = name;
            Price = price;
        }
    }
}
