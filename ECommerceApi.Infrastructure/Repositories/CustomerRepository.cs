using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Interfaces;
using ECommerceApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace ECommerceApi.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context) { }

        public async Task<Customer?> GetByEmailAsync(string email) => 
            await _dbSet.FirstOrDefaultAsync(c => c.Email == email);

    }
}
