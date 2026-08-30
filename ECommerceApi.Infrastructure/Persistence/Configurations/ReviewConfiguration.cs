using ECommerceApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Infrastructure.Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Comment).HasMaxLength(1000);

            
            builder.HasOne<Product>()
                   .WithMany()
                   .HasForeignKey(r => r.ProductId);

            
            builder.HasOne<Customer>()
                   .WithMany()
                   .HasForeignKey(r => r.CustomerId);
        }
    }
}
