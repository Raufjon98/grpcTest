using Eshop.Application.Customers;
using Eshop.Application.Customers.Commands;
using Eshop.Application.Customers.Queries;
using Grpc.Core;
using GrpcGenerated;
using MediatR;

namespace Eshop.webApi.GrpcServices;

public class CustomerGrcpService
{
    private readonly IMediator _mediator;

    public CustomerGrcpService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CreateCustomerResponse> CreateCustomer(CreateCustomerRequest request,
        ServerCallContext context)
    {
        var command = new CreateCustomerCommand(new CustomerDto
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
        });
        var result = await _mediator.Send(command);

        return new CreateCustomerResponse
        {
            Id = result.Id,
            Name  = result.Name,
            Email = result.Email,
            Phone = result.Phone,
        };
    }

    public async Task<CustomerList> GetCustomers(Empty request, ServerCallContext context)
    {
        var query = new GetCustomersQuery();
        var result = await _mediator.Send(query);
        var response = new CustomerList();
        foreach (var customer in result)
        {
            response.Customers.Add(new CreateCustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
            });
        }
        return response;
    }

    public async Task<CreateCustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest request, ServerCallContext context)
    {
        CustomerDto customer = new CustomerDto
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
        };
        var command = new UpdateCustomerCommand(id, customer);
        var result = await _mediator.Send(command);
        return new CreateCustomerResponse
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email,
            Phone = result.Phone,
        };
    }
}