using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Mappings;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Application.Products.Queries.GetProductDetail;

namespace HangangRamyeon.Application.Products.Queries.GetProducts;

public record GetProductsQuery : IRequest<Result>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public Guid? ShopId { get; init; }
    public string? SearchString { get; init; }
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Shop)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchString))
        {
            query = query.Where(x =>
                x.Name.Contains(request.SearchString) ||
                x.Code.Contains(request.SearchString));
        }

        if (request.ShopId.HasValue)
        {
            query = query.Where(x => x.ShopId == request.ShopId.Value);
        }

        var products = await query
            .OrderBy(x => x.Name)
            .Select(x => _mapper.Map<ProductDto>(x))
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        return Result.Success(products);
    }
}
