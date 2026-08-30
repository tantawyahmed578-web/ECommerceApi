using ECommerceApi.Domain.Common;
using ECommerceApi.Domain.Interfaces;
using ECommerceApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id); // Use FindAsync for primary key lookup

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _dbSet.ToListAsync(); // Use ToListAsync to retrieve all entities

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity); // Use AddAsync to add a new entity

        public void Update(T entity) => _dbSet.Update(entity); // Use Update to update an existing entity

        public void Delete(T entity) => _dbSet.Remove(entity);  // Use Remove to delete an entity
    }
}
