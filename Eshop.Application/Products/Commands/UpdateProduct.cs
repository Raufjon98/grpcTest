using Eshop.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Products.Commands;

public record UpdateProductCommand(int productId, ProductDto Product) : IRequest<ProductVM>;
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductVM>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductVM> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.productId);
        ArgumentNullException.ThrowIfNull(product);
        product.Name = request.Product.Name;
        product.Description = request.Product.Description;
        product.Price = request.Product.Price;
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return new ProductVM
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
        };
    }
}