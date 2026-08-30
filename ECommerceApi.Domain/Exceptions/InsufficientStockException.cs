using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Exceptions
{
    public class InsufficientStockException : DomainException
    {
        public InsufficientStockException(string productName, int available, int requested)
           : base($"Insufficient stock for '{productName}'. Available: {available}, requested: {requested}.") { }

    }
}
