using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Customers.Queries;

public record GetCustomersQuery : IRequest<List<Customer>>;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<Customer>>
{
    private readonly IApplicationDbContext _context;

    public GetCustomersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _context.Customers.ToListAsync();
        return customers;
    }
}