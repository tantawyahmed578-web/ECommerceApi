using ECommerceApi.Application.DTOs.OrderItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(int customerId, CreateOrderDto dto);
        Task<OrderDto?> GetByIdAsync(int id);
        Task<IReadOnlyList<OrderDto>> GetByCustomerAsync(int customerId);
    }
}
