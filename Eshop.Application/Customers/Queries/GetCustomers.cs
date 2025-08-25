using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Customers.Queries;

public record GetCustomersQuery : IRequest<List<CustomerVM>>;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerVM>>
{
    private readonly IApplicationDbContext _context;

    public GetCustomersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<CustomerVM>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _context.Customers.ToListAsync();
        List<CustomerVM> result = new List<CustomerVM>();
        foreach (var customer in customers)
        {
            result.Add(new CustomerVM
            {
                Id = customer.Id,
                Email = customer.Email,
                Name = customer.Name,
                Phone = customer.Phone,
            });
        }
        return result;
    }
}