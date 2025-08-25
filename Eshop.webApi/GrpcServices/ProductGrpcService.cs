

using Eshop.Application.Categories.Commands;
using Eshop.Application.Products;
using Eshop.Application.Products.Commands;
using Eshop.Application.Products.Queries;
using Grpc.Core;
using GrpcGenerated;
using MediatR;

namespace Eshop.webApi.GrpcServices;

public class ProductGrpcService : ProductService.ProductServiceBase
{
    private readonly IMediator  _mediator;

    public ProductGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
    {
        var command = new CreateProductCommand(new ProductDto
        {
            Name = request.Name,
            Description = request.Description,
            Price =(decimal)request.Price,
        });
        var result = await _mediator.Send(command);
        
        return new CreateProductResponse
        {
            Name = result.Name,
            Description = result.Description,
            Price =(double)result.Price,
        };
    }

    public override async Task<ProductList> GetProducts(Empty request, ServerCallContext context)
    {
        var query = new GetProductsQuery();
        var result = await _mediator.Send(query);
        var response = new ProductList();
        foreach (var item in result)
        {
            response.Products.Add(new CreateProductResponse
            {
                Name = item.Name,
                Description = item.Description,
                Price = (double)item.Price,
            });
        }
        return response;
    }

    public async Task<bool> DeleteCategory(int categoryId, ServerCallContext context)
    {
        var command = new DeleteProductCommand(categoryId);
        var result = await _mediator.Send(command);
        return result;
    }

    public override async Task<CreateProductResponse> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
    {
        var product = new ProductDto
        {
            Name = request.Name,
            Description = request.Description,
        };
        var command = new UpdateProductCommand(request.ProductId, product);
        var result = await _mediator.Send(command);
        return new CreateProductResponse
        {
            Name = result.Name,
            Description = result.Description,
            Price = (double)result.Price,
            Id = result.Id
        };
    }
}