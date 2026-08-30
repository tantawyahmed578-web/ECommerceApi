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
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {

        public ReviewRepository(AppDbContext context) : base(context) { }
       
        public async Task<IReadOnlyList<Review>> GetByProductAsync(int productId)=> 
            await _dbSet.Where(r => r.ProductId == productId).ToListAsync();

    }
}
