using Eshop.Application.Categories.Commands;
using Eshop.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Customers.Commands;

public record UpdateCustomerCommand(int customerId, CustomerDto Customer) : IRequest<CustomerVM>;
public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerVM>
{
   private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext  context)
    {
           _context = context;
    }
    public async Task<CustomerVM> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == request.customerId);
        ArgumentNullException.ThrowIfNull(customer);
        customer.Name = request.Customer.Name;
        customer.Email = request.Customer.Email;
        customer.Phone = request.Customer.Phone;
        
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync(cancellationToken);
        return new CustomerVM
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
        };
    }
}