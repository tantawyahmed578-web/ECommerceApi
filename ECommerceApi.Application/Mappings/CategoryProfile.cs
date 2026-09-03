using AutoMapper;
using ECommerceApi.Application.DTOs.CategoryDto;
using ECommerceApi.Domain.Entities;


namespace ECommerceApi.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>(); //

        }
    }
}
