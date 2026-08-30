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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");

            builder.HasOne(i => i.Product)
                   .WithMany()
                   .HasForeignKey(i => i.ProductId);

            builder.Ignore(i => i.LineTotal); // also computed, also not stored
        }
    }
}
