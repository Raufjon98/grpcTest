using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Carts.Queries;

public record GetCartsQuery : IRequest<List<Cart>>;
public class GetCartsQueryHandler : IRequestHandler<GetCartsQuery, List<Cart>>
{
    private readonly IApplicationDbContext _context;

    public GetCartsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Cart>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
    {
        var carts = await _context.Carts.ToListAsync();
        return carts;
    }
}