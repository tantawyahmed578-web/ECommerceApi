using ECommerceApi.Domain.Interfaces;
using ECommerceApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IProductRepository? _products;
        private ICategoryRepository? _categories;
        private ICustomerRepository? _customers;
        private IOrderRepository? _orders;
        private IReviewRepository? _reviews;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProductRepository Products => _products ??= new ProductRepository(_context);
        public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);
        public ICustomerRepository Customers => _customers ??= new CustomerRepository(_context);
        public IOrderRepository Orders => _orders ??= new OrderRepository(_context); // 
        public IReviewRepository Reviews => _reviews ??= new ReviewRepository(_context); // Lazy initialization of the repositories

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync(); // Save changes to the database and return the number of affected rows

        public void Dispose() => _context.Dispose(); // Dispose the context when the UnitOfWork is disposed
    }
}
