using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using MediatR;

namespace Eshop.Application.Products.Commands;

public record CreateProductCommand(ProductDto Product) : IRequest<ProductVM>;
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductVM>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ProductVM> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Product.Name,
            Description = request.Product.Description,
            Price = request.Product.Price,
            CategoryId = request.Product.CategoryId,
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        return new ProductVM
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
        };
    }
}