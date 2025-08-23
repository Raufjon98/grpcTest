using Eshop.Application.Categories.Queries;
using Eshop.Application.Common.Interfaces;
using Eshop.Domain.Entities;
using MediatR;

namespace Eshop.Application.Categories.Commands;

public record CreateCategoryCommand(CategoryDto Category) : IRequest<CategoryVM>;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryVM>
{ 
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<CategoryVM> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = new Category
        {
            Name = request.Category.Name,
            Description = request.Category.Description,
        };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);
        CategoryVM result = new CategoryVM
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
        return result;
    }
}