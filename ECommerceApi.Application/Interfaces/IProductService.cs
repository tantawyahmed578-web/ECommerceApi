using ECommerceApi.Application.DTOs;
using ECommerceApi.Application.DTOs.pagination_and_filtering;
using ECommerceApi.Application.DTOs.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(int categoryId);
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task UpdateAsync(int id, UpdateProductDto dto);
        Task DeleteAsync(int id);
        Task<PagedResultDto<ProductDto>> GetPagedAsync(ProductQueryParameters queryParams);
    }
}
