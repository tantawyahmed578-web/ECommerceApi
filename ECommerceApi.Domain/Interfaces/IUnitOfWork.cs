using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IReviewRepository Reviews { get; }

        Task<int> SaveChangesAsync();
    }
}
