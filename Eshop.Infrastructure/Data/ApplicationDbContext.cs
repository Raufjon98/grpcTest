using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public override async  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    => await base.SaveChangesAsync(cancellationToken);
}