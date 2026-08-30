using ECommerceApi.Domain.Common;
using ECommerceApi.Domain.Enums;
using ECommerceApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Pending; // Default to Pending when created

        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly(); 

        public decimal Total => _items.Sum(i => i.LineTotal); // Calculate total based on order items

        private Order() { }

        public Order(int customerId)
        {
            CustomerId = customerId;
        }

        public void AddItem(Product product, int quantity) // AddItem method now takes a Product object instead of just productId
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive.");

            product.DecreaseStock(quantity); // enforces the stock rule at order time

            _items.Add(new OrderItem(product.Id, quantity, product.Price));
        }

        public void Confirm() // Confirm method to change the order status to Confirmed
        {
            if (_items.Count == 0)
                throw new DomainException("Cannot confirm an order with no items.");

            Status = OrderStatus.Confirmed;
        }
    }
}
