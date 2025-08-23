using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using MediatR;

namespace Eshop.Application.Orders.Commands;

public record CreateOrderCommand (OrderDto Order): IRequest<OrderVM>;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderVM>
{
    private readonly IApplicationDbContext  _context;

    public CreateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<OrderVM> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = new Order
        {
            CustomerId = request.Order.CustomerId,
            CartId = request.Order.CartId,
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
        return new OrderVM
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            CartId = order.CartId
        };
    }
}