using Eshop.Application.Orders;
using Eshop.Application.Orders.Commands;
using Eshop.Application.Orders.Queries;
using Grpc.Core;
using GrpcGenerated;
using MediatR;

namespace Eshop.webApi.GrpcServices;

public class OrderGrpcService : OrderService.OrderServiceBase
{
    private readonly IMediator  _mediator;

    public OrderGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
    {
        var command = new CreateOrderCommand(new OrderDto
        {
            CartId = request.CartId,
            CustomerId = request.CustomerId,
        });
        var result = await _mediator.Send(command);
        return new CreateOrderResponse
        {
            Id = result.Id,
            CustomerId = result.CustomerId,
            CartId = result.CartId,
        };
    }

    public override async Task<OrderList> GetOrders(OrderEmpty request, ServerCallContext context)
    {
        var query = new GetOrdersQuery();
        var result =await _mediator.Send(query);
        var response = new OrderList();
        foreach (var item in result)
        {
            response.Orders.Add(new CreateOrderResponse
            {
                Id = item.Id,
                CustomerId = item.CustomerId,
                CartId = item.CartId,
            });
        }
        return response;
    }

    public async Task<CreateOrderResponse> updateOrder(UpdateOrderRequest request, ServerCallContext context)
    {
        var command = new UpdateOrderCommand(request.OrderId, new OrderDto{CartId = request.CartId, CustomerId = request.CustomerId});
        var result = await _mediator.Send(command);
        return new CreateOrderResponse
        {
            Id = result.Id,
            CustomerId = result.CustomerId,
            CartId = result.CartId,
        };
    }
}