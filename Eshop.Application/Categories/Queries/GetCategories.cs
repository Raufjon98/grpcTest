using Eshop.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Categories.Queries;

public record GetCategoriesQuery : IRequest<IEnumerable<CategoryVM>>;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryVM>>
{
    private readonly IApplicationDbContext _context;

    public GetCategoriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<CategoryVM>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories.ToListAsync();
        var result = new List<CategoryVM>();
        foreach (var category in categories)
        {
            CategoryVM categoryVm = new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
            result.Add(categoryVm);
        }
        return result;
    }
}