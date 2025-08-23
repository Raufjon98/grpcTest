using Eshop.Application.Categories.Queries;
using Eshop.Application.Common.Interfaces;
using Eshop.Application.Customers;
using Eshop.Application.Products;
using Eshop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Categories.Commands;

public record UpdateCategoryCommand(int CategoryId, CategoryDto Category) : IRequest<CategoryVM>;
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryVM>
{
    private readonly IApplicationDbContext  _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;        
    }
    public async Task<CategoryVM> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.CategoryId);
        ArgumentNullException.ThrowIfNull(category);
        
        category.Name = request.Category.Name;
        category.Description = request.Category.Description;
         _context.Products.Update(category);
         await _context.SaveChangesAsync(cancellationToken);
        CategoryVM vm = new CategoryVM
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
        };      
        return vm;
    }
}