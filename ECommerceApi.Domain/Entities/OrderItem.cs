using ECommerceApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; } = null!;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; } // snapshot at time of order

        private OrderItem() { }

        internal OrderItem(int productId, int quantity, decimal unitPrice) // internal constructor for creating a new order item
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal LineTotal => Quantity * UnitPrice;  // Calculate line total based on quantity and unit price
    }
}
