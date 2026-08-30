using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Interfaces;
using ECommerceApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }


        public async Task<IReadOnlyList<Product>> GetByCategoryAsync(int categoryId) =>
         await _dbSet.Where(p => p.CategoryId == categoryId).ToListAsync();

        public async Task<IReadOnlyList<Product>> SearchByNameAsync(string keyword) =>
            await _dbSet.Where(p => p.Name.Contains(keyword)).ToListAsync();
    }
}
