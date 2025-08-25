using Eshop.Application.Orders;
using Eshop.Application.Orders.Commands;
using Eshop.Application.Orders.Queries;
using Eshop.Domain.Entities;
using Eshop.webApi.Structure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.webApi.Endpoints;

public class Orders : EndpointGroupBase
{
    public override string Prefix => "/orders";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        group.MapGet("/", GetOrders);
        group.MapPost("/", CreateOrder);
        group.MapPut("/{id}", UpdateOrder);
    }
    
    public async Task<IResult> CreateOrder([FromBody]OrderDto order, [FromServices] ISender sender)
    {
        var result =await sender.Send(new CreateOrderCommand(order));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> UpdateOrder([FromRoute]int id, [FromBody]OrderDto order, [FromServices]ISender sender)
    {
        var result = await sender.Send(new UpdateOrderCommand(id, order));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetOrders([FromServices] ISender sender)
    {
        var result = await sender.Send(new GetOrdersQuery());
        return TypedResults.Ok(result);
    }
}