using Eshop.Application.Carts;
using Eshop.Application.Carts.Commands;
using Eshop.Application.Carts.Queries;
using Eshop.webApi.Structure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.webApi.Endpoints;

public class Carts : EndpointGroupBase
{
    public override string Prefix => "/carts";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        group.MapGet("/", GetCarts);
        group.MapPost("/", CreateCart);
        group.MapPut("/{id}", UpdateCart);
    }

    public async Task<IResult> CreateCart([FromBody] CartDto cart, [FromServices] ISender sender)
    {
        var result = await sender.Send(new CreateCartCommand(cart));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> UpdateCart([FromRoute] int id, [FromBody] CartDto cart, [FromServices] ISender sender)
    {
        var result = await sender.Send(new UpdateCartCommand(id, cart));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetCarts([FromServices] ISender sender)
    {
        var result = await sender.Send(new GetCartsQuery());
        return TypedResults.Ok(result);
    }
}