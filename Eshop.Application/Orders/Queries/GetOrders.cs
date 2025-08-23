using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Orders.Queries;

public record GetOrdersQuery : IRequest<List<Order>>;
public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<Order>>
{
    private readonly IApplicationDbContext _context;

    public GetOrdersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var Orders = await _context.Orders.ToListAsync();
        return Orders;
    }
}