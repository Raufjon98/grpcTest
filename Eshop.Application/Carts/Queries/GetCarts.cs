using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Carts.Queries;

public record GetCartsQuery : IRequest<List<CartVM>>;
public class GetCartsQueryHandler : IRequestHandler<GetCartsQuery, List<CartVM>>
{
    private readonly IApplicationDbContext _context;

    public GetCartsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<CartVM>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
    {
        var carts = await _context.Carts.ToListAsync();
        List<CartVM> result = new List<CartVM>();
        foreach (var cart in carts)
        {
            result.Add(new CartVM
            {
                Id = cart.Id,
                CustomerId = cart.CustomerId,
            });
        }   
        return result;
    }
}