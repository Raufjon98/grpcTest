using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using MediatR;

namespace Eshop.Application.Customers.Commands;

public record CreateCustomerCommand(CustomerDto Customer) : IRequest<CustomerVM>;
public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerVM>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<CustomerVM> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer customer = new Customer
        {
            Name = request.Customer.Name,
            Email = request.Customer.Email,
            Phone = request.Customer.Phone,
        };
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        CustomerVM customerVM = new CustomerVM
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
        };
        return customerVM;
    }
}