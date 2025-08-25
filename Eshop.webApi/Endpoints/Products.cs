using Eshop.Application.Products;
using Eshop.Application.Products.Commands;
using Eshop.Application.Products.Queries;
using Eshop.webApi.Structure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.webApi.Endpoints;

public class Products : EndpointGroupBase
{
    public override string Prefix => "/products";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        
    }

    public async Task<IResult> CreateProduct([FromBody] ProductDto product, [FromServices] ISender sender)
    {
        var result = await sender.Send(new CreateProductCommand(product));
        return TypedResults.Ok(result);;
    }

    public async Task<IResult> UpdateProduct([FromRoute] int Id, [FromQuery] ProductDto product,
        [FromServices] ISender sender)
    {
        var result = await sender.Send(new UpdateProductCommand(Id, product));
        return TypedResults.Ok(result);;
    }

    public async Task<IResult> GetProducts([FromServices] ISender sender)
    {
        var result = await sender.Send(new GetProductsQuery());
        return TypedResults.Ok(result);
    }
}