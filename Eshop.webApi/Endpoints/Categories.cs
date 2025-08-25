using Eshop.Application.Categories.Commands;
using Eshop.Application.Categories.Queries;
using Eshop.webApi.Structure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.webApi.Endpoints;

public class Categories : EndpointGroupBase
{
    public override string Prefix => "/categories";
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(Prefix);
        group.MapGet("/", GetCategories);
        group.MapPost("/", CreateCategory);
        group.MapDelete("/{id}", DeleteCategory);
        group.MapPut("/{id}",  UpdateCategory);
    }

    public async Task<IResult> UpdateCategory([FromQuery] int id, [FromBody] CategoryDto category, [FromServices] ISender sender )
    {
        var result = await sender.Send(new UpdateCategoryCommand(id, category)); 
        return TypedResults.Ok(result);
    }
    public async Task<IResult> CreateCategory([FromBody] CategoryDto category, [FromServices] ISender sender)
    {
        var result = await sender.Send(new CreateCategoryCommand(category));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> DeleteCategory([FromRoute] int id, [FromServices] ISender sender)
    {
        var result = await sender.Send(new DeleteCategoryCommand(id));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetCategories([FromServices] ISender sender)
    {
        var result = await sender.Send(new GetCategoriesQuery());
        return TypedResults.Ok(result);
    }
}