using Eshop.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Orders.Commands;

public record UpdateOrderCommand(int orderId, OrderDto Order) : IRequest<OrderVM>;
public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderVM>
{
    private readonly IApplicationDbContext _context;

    public UpdateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<OrderVM> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o=>o.Id == request.orderId);
        ArgumentNullException.ThrowIfNull(order);
        
        order.CustomerId = request.Order.CustomerId;
        order.CartId = request.Order.CartId;
        return new OrderVM
        {
            CustomerId = order.CustomerId,
            CartId = order.CartId,
        };
    }
}