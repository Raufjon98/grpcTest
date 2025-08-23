using Eshop.Application.Carts;
using Eshop.Application.Carts.Commands;
using Eshop.Application.Carts.Queries;
using Grpc.Core;
using GrpcGenerated;
using MediatR;

namespace Eshop.webApi.GrpcServices;

public class CartGrcpService : CartService.CartServiceBase
{ 
    private readonly IMediator _mediator;

    public CartGrcpService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CreateCartResponse> CreateCart(CreateCartRequest request, ServerCallContext context)
    {
        var command = new CreateCartCommand(new CartDto
        {
            CustomerId = request.CustomerId
        });
        
        var result = await _mediator.Send(command);

        return new CreateCartResponse
        {
            Id = result.Id,
            CustomerId = result.CustomerId
        };
    }

    public async Task<CartList> GetCarts(Empty request, ServerCallContext context)
    {
        var query = new GetCartsQuery();
        var result = await _mediator.Send(query);
        var response = new CartList();
        foreach (var item in result)
        {
            response.Carts.Add(new CreateCartResponse
            {
                Id = item.Id,
                CustomerId = item.CustomerId
            });
        }
        return response;
    }

    public async Task<CreateCartResponse> UpdateCart(UpdateCartRequest request, ServerCallContext context)
    {
       var command =new UpdateCartCommand(request.CartId, new CartDto{CustomerId = request.CustomerId});
       var result = await _mediator.Send(command);
      return  new CreateCartResponse
      {
          Id = result.Id,
          CustomerId = result.CustomerId
      };
    }
}