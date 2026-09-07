using AutoMapper;
using ECommerceApi.Application.DTOs.OrderItemDto;
using ECommerceApi.Application.Interfaces;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Exceptions;
using ECommerceApi.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderService> _logger;
    private readonly IBasketRepository _basketRepository; // إضافة الإنترفيس الخاص بـ Redis

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderService> logger, IBasketRepository basketRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _basketRepository = basketRepository;
    }

    public async Task<OrderDto> CreateOrderAsync(int customerId, CreateOrderDto dto)
    {
        // 1. جلب السلة من Redis
        var basket = await _basketRepository.GetBasketAsync(dto.BasketId)
            ?? throw new DomainException("Basket not found or expired.");

        if (basket.Items.Count == 0)
            throw new DomainException("Cannot create an order from an empty basket.");

        var order = new Order(customerId);

        // 2. بناء الطلب من السلة والتأكد من المنتجات من SQL
        foreach (var basketItem in basket.Items)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(basketItem.Id)
                ?? throw new DomainException($"Product with id {basketItem.Id} was not found.");

            // AddItem بتخصم الـ Stock تلقائياً وبتضيف المنتج للطلب
            order.AddItem(product, basketItem.Quantity);
        }

        order.Confirm();

        // 3. حفظ الطلب في قاعدة البيانات (SQL Server)
        await _unitOfWork.Orders.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Order {OrderId} placed by customer {CustomerId}, total {Total}", order.Id, customerId, order.Total);

        // 4. مسح السلة من Redis بعد إتمام الشراء بنجاح
        await _basketRepository.DeleteBasketAsync(dto.BasketId);

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