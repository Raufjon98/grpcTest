using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using MediatR;

namespace Eshop.Application.Carts.Commands;

public record CreateCartCommand(CartDto Cart) : IRequest<CartVM>;
public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, CartVM>
{
    private readonly IApplicationDbContext _context;

    public CreateCartCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<CartVM> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        Cart cart = new Cart
        {
            
            CustomerId = request.Cart.CustomerId,
        };
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync(cancellationToken);
        return new CartVM
        {
            Id = cart.Id,
            CustomerId = cart.CustomerId
        };
    }
}