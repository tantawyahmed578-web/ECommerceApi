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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }
        
        public async Task<IReadOnlyList<Order>> GetByCustomerAsync(int customerId)=>
           await _dbSet.Include(o => o.Items) // Include the related OrderItems for each Order
                     .ThenInclude(i => i.Product) // Include the related Product entity for each OrderItem
                     .Where(o => o.CustomerId == customerId)    // Filter orders by the specified customerId
                     .ToListAsync(); 

    }
}
