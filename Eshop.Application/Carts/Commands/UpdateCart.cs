using Eshop.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Carts.Commands;

public record UpdateCartCommand(int cartId, CartDto Cart) : IRequest<CartVM>;
public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, CartVM>
{
    private readonly IApplicationDbContext _context;

    public UpdateCartCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<CartVM> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c=>c.Id == request.cartId);
        ArgumentNullException.ThrowIfNull(cart);
        cart.CustomerId = request.Cart.CustomerId;
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync(cancellationToken);
        return new CartVM
        {
            Id = cart.Id
        };
    }
 }