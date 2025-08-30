using Eshop.Application.Common.Interfaces;
using Eshop.Infrastructure.Data;
using Eshop.webApi.GrpcServices;
using Eshop.webApi.Structure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
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
app.UseHttpsRedirection();
app.MapEndpoints();
app.MapGet("/", () => "Hello World from Docker!");
app.Run();

