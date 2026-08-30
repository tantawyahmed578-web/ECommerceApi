using ECommerceApi.Domain.Common;
using ECommerceApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Entities
{
    public class Review : BaseEntity
    {
        public int ProductId { get; private set; }
        public int CustomerId { get; private set; }
        public int Rating { get; private set; }
        public string? Comment { get; private set; }

        private Review() { } 

        public Review(int productId, int customerId, int rating, string? comment) // public constructor for creating a new review
        {
            if (rating < 1 || rating > 5) // Validate rating is between 1 and 5
                throw new DomainException("Rating must be between 1 and 5.");

            ProductId = productId;
            CustomerId = customerId;
            Rating = rating;
            Comment = comment;
        }
    }
}
