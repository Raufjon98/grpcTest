using Eshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.Data.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.HasOne(o => o.Cart)
            .WithOne()
            .HasForeignKey<Order>(o=>o.CartId);
        
        builder.HasOne(o=>o.Customer)
            .WithMany()
            .HasForeignKey(o=>o.CustomerId);
    }
}