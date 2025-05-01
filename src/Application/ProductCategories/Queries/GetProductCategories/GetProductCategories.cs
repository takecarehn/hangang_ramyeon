using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Mappings;
using HangangRamyeon.Application.Common.Models;

namespace HangangRamyeon.Application.ProductCategories.Queries.GetProductCategories;

public record GetProductCategoriesQuery : IRequest<Result>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SearchString { get; init; }
}

public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ProductCategories.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchString))
        {
            query = query.Where(x =>
                x.Name.Contains(request.SearchString) ||
                x.Code.Contains(request.SearchString)
            );
        }

        var productCategories = await query
            .OrderBy(x => x.Name)
            .Select(x => _mapper.Map<ProductCategoryDto>(x))
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result.Success(productCategories);
    }
}
