using AutoMapper;
using ECommerceApi.Application.DTOs;
using ECommerceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Mappings
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();  // Mapping from Product entity to ProductDto
        }
    }
}
