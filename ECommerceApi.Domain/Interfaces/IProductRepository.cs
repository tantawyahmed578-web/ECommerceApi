using ECommerceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetByCategoryAsync(int categoryId);
        Task<IReadOnlyList<Product>> SearchByNameAsync(string name);
        Task<(IReadOnlyList<Product> Items, int TotalCount)> GetPagedAsync( int pageNumber, int pageSize, int? categoryId, string? searchTerm);
    }
}
