using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Interfaces;
using ECommerceApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public override async Task<Order?> GetByIdAsync(int id) =>
            await _dbSet.Include(o => o.Items)
                         .ThenInclude(i => i.Product)
                         .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<IReadOnlyList<Order>> GetByCustomerAsync(int customerId) =>
           await _dbSet.Include(o => o.Items)
                     .ThenInclude(i => i.Product)
                     .Where(o => o.CustomerId == customerId)
                     .ToListAsync();
    }
}
