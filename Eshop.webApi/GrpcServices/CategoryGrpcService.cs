using Eshop.Application.Categories.Commands;
using Eshop.Application.Categories.Queries;
using Grpc.Core;
using GrpcGenerated;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Eshop.webApi.GrpcServices;

public class CategoryGrpcService : CategoryService.CategoryServiceBase
{
    private readonly IMediator  _mediator;

    public CategoryGrpcService(IMediator mediator)
    {
        _mediator = mediator;   
    }

    public override async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request,
        ServerCallContext context)
    {
        var command = new CreateCategoryCommand(new CategoryDto
        {
            Name = request.Name,
            Description = request.Description,
        });
        var result = await _mediator.Send(command);
        return new CreateCategoryResponse
        {
            Name = result.Name,
            Description = result.Description
        };
    }

    public async Task<CategoryList> GetCategories(Empty request, ServerCallContext context)
    {
        var query = new GetCategoriesQuery();
        var result =await _mediator.Send(query);
        var response = new CategoryList();
        foreach (var item in result)
        {
            response.Categories.Add(new CreateCategoryResponse
            {
                Name = item.Name,
                Description = item.Description
            });
        }
        return response;
    }

    public async Task<bool> DeleteCategory(int categoryId, Empty request, ServerCallContext context)
    {
        var query = new DeleteCategoryCommand(categoryId);
        var result = await _mediator.Send(query);
        return result;
    }

    public override async Task<CreateCategoryResponse> UpdateCategory(UpdateCategoryRequest request, ServerCallContext context)
    {
        var category = new CategoryDto
        {
            Name = request.Name,
            Description = request.Description
        };
        var command = new UpdateCategoryCommand(request.CategoryId, category);
        var result = await _mediator.Send(command);
        return new CreateCategoryResponse
        {
            Name = result.Name,
            Description = result.Description
        };
    }
}