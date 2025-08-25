using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Orders.Queries;

public record GetOrdersQuery : IRequest<List<OrderVM>>;
public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderVM>>
{
    private readonly IApplicationDbContext _context;

    public GetOrdersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<OrderVM>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var Orders = await _context.Orders.ToListAsync();
        List<OrderVM> result =  new List<OrderVM>();
        foreach (var order in Orders)
        {
            result.Add(new OrderVM
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CartId = order.CartId,
            });
        }
        return result;
    }
}