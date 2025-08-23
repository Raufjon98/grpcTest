 
using Eshop.Application.Carts;
using Eshop.Application.Carts.Commands;
using Eshop.Application.Carts.Queries;
using Eshop.Application.Categories.Commands;
using Eshop.Application.Categories.Queries;
using Eshop.Application.Common.Interfaces;
using Eshop.Application.Customers;
using Eshop.Application.Customers.Commands;
using Eshop.Application.Customers.Queries;
using Eshop.Application.Orders;
using Eshop.Application.Orders.Commands;
using Eshop.Application.Products;
using Eshop.Application.Products.Commands;
using Eshop.Application.Products.Queries;
using Eshop.Domain.Entities;
using Eshop.Infrastructure.Data;
using Eshop.webApi.GrpcServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    // options.UseInMemoryDatabase("eshopdb"));
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<IApplicationDbContext>(provider => 
    provider.GetRequiredService<ApplicationDbContext>());
builder.Services.AddMediatR(typeof(Eshop.Application.AssemblyReference).Assembly);
builder.Services.AddGrpc();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.MapGrpcService<ProductGrpcService>();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
        options.WithTitle("Eshop.webApi"));
}
app.MapGet("/", () => "Hello Eshop!");
app.MapPost("/products", async (ProductDto product, ISender _sender) =>
{
var result = await _sender.Send(new CreateProductCommand(product));
return result;
});
app.MapGet("/products", async (ISender sender) =>
{
    var result = await sender.Send(new GetProductsQuery());
    return result;
});
app.MapDelete("/products/{id:int}", async (int id, ISender _sender) =>
{
    var result = await _sender.Send(new DeleteProductCommand(id));
    return result;
});

app.MapPut("/products/{id:int}", async (int id, ProductDto Product, ISender _sender) =>
{
    var result = await _sender.Send(new UpdateProductCommand(id, Product));
    return result;
});

app.MapPost("/categories", async (CategoryDto category, ISender _sender) =>
{
    var result = await _sender.Send(new CreateCategoryCommand(category));
    return result;
});
app.MapGet("/categories", async (ISender sender) =>
{
    var result = await sender.Send(new GetCategoriesQuery());
    return result;
});
app.MapDelete("/categories", async (int categoryId, ISender sender) =>
{
    var result = await sender.Send(new DeleteCategoryCommand(categoryId));
    return result;
});

app.MapPut("/catgories/{id:int}", async (int id, CategoryDto category, ISender sender) =>
{
    var result = await sender.Send(new UpdateCategoryCommand(id, category));
});

app.MapPost("/carts", async (CartDto cart, ISender _sender) =>
{
    var result = await _sender.Send((new CreateCartCommand(cart)));
    return result;
});
app.MapGet("/carts", async (ISender _sender) =>
{
    var result = await _sender.Send(new GetCartsQuery());
    return result;
});

app.MapPut("/carts/{id:int}", async (int id, CartDto cart, ISender _sender) =>
{
    var result = await _sender.Send((new UpdateCartCommand(id, cart)));
    return result;
});

app.MapPost("/customers", async (CustomerDto customer, ISender _sender) =>
{
    var result = await _sender.Send(new CreateCustomerCommand(customer));
    return result;
});
app.MapGet("/customers", async (ISender _sender) =>
{
    var result = await _sender.Send(new GetCustomersQuery());
    return result;
});
app.MapPost("/customers/{id:int}", async (int id, CustomerDto customer, ISender _sender) =>
{
    var result = await _sender.Send(new UpdateCustomerCommand(id, customer));
});

app.MapPost("/orders", async (OrderDto order, ISender _sender) =>
{
    var result = _sender.Send(new CreateOrderCommand(order));
    return result;
});

app.MapPut("/orders/{id:int}", async (int id, OrderDto order, ISender _sender) =>
{
    var result = await _sender.Send(new UpdateOrderCommand(id, order));
    return result;
});

app.Run();

