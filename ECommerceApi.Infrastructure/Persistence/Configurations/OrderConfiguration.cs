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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Status).HasConversion<string>();

            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey(i => i.OrderId);

            builder.Navigation(o => o.Items).UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(o => o.Total);

            
            builder.HasOne<Customer>()
                   .WithMany()
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
