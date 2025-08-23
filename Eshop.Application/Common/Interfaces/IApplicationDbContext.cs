using Eshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Cart> Carts { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Customer> Customers { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}