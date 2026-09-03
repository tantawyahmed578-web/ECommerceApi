using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.DTOs.OrderItemDto
{
    public class CreateOrderItemDto // DTO for creating an order item
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }


}
