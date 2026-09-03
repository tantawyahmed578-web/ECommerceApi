using AutoMapper;
using ECommerceApi.Application.DTOs.OrderItemDto;
using ECommerceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                //Status.ToString() converts the OrderStatus enum to a readable string like "Confirmed" for the API response, instead of the client seeing a raw number.
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())); // Map enum to string

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name)); // Map Product.Name to ProductName
        }
    }
}
