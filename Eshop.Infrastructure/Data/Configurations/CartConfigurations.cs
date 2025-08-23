using Eshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.Data.Configurations;

public class CartConfigurations : IEntityTypeConfiguration<Cart>

{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasOne(c => c.Customer)
            .WithOne()
            .HasForeignKey<Cart>(c => c.CustomerId);
    }
}