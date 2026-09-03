using AutoMapper;
using ECommerceApi.Application.DTOs;
using ECommerceApi.Application.DTOs.pagination_and_filtering;
using ECommerceApi.Application.DTOs.ProductDto;
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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return product is null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(int categoryId)
        {
            var products = await _unitOfWork.Products.GetByCategoryAsync(categoryId);
            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product(dto.Name, dto.Price, dto.StockQuantity, dto.CategoryId);

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id)
                ?? throw new DomainException($"Product with id {id} was not found.");

            product.UpdateDetails(dto.Name, dto.Price);

            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id)
                ?? throw new DomainException($"Product with id {id} was not found.");

            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResultDto<ProductDto>> GetPagedAsync(ProductQueryParameters queryParams)
        {
            var (items, totalCount) = await _unitOfWork.Products.GetPagedAsync(
                queryParams.PageNumber, queryParams.PageSize, queryParams.CategoryId, queryParams.SearchTerm);

            return new PagedResultDto<ProductDto>
            {
                Items = _mapper.Map<List<ProductDto>>(items),
                TotalCount = totalCount,
                PageNumber = queryParams.PageNumber,
                PageSize = queryParams.PageSize
            };
        }
    }
}
