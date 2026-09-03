using AutoMapper;
using ECommerceApi.Application.DTOs.OrderItemDto;
using ECommerceApi.Application.Interfaces;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Exceptions;
using ECommerceApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Services
{
    public class OrderService : IOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OrderDto> CreateOrderAsync(int customerId, CreateOrderDto dto)
        {
            if (dto.Items is null || dto.Items.Count == 0)
                throw new DomainException("An order must contain at least one item.");

            var order = new Order(customerId);

            foreach (var item in dto.Items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId) 
                    ?? throw new DomainException($"Product with id {item.ProductId} was not found.");

                order.AddItem(product, item.Quantity);
            }

            order.Confirm();

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            return order is null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<IReadOnlyList<OrderDto>> GetByCustomerAsync(int customerId)
        {
            var orders = await _unitOfWork.Orders.GetByCustomerAsync(customerId);
            return _mapper.Map<IReadOnlyList<OrderDto>>(orders);
        }

      
    }
}
