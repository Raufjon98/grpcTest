using Eshop.Application.Customers;
using Eshop.Application.Customers.Commands;
using Eshop.Application.Customers.Queries;
using Eshop.webApi.Structure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.webApi.Endpoints;

public class Customers : EndpointGroupBase
{
    public override string Prefix => "/customers";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        group.MapGet("/", GetCustomers);
        group.MapPost("/", CreateCustomer);
        group.MapPut("/{id}", UpdateCustomer);
    }

    public async Task<IResult> CreateCustomer([FromBody] CustomerDto customer, [FromServices] ISender sender)
    {
        var result = await sender.Send(new CreateCustomerCommand(customer));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> UpdateCustomer([FromRoute] int id, [FromBody] CustomerDto customer,
        [FromServices] ISender sender)
    {
        var result = await sender.Send(new UpdateCustomerCommand(id, customer));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetCustomers([FromServices]  ISender sender)
    {
        var result = await sender.Send(new GetCustomersQuery());
        return TypedResults.Ok(result);
    }
}