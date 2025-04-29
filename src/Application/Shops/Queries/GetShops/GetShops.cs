using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Mappings;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Application.Shops.Queries.GetShopDetail;

namespace HangangRamyeon.Application.Shops.Queries.GetShops;
public record GetShopsQuery : IRequest<Result>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SearchString { get; init; }
}

public class GetShopsQueryHandler : IRequestHandler<GetShopsQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShopsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result> Handle(GetShopsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Shops.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchString))
        {
            query = query.Where(x =>
                x.Name.Contains(request.SearchString) ||
                x.Code.Contains(request.SearchString) ||
                x.Address.Contains(request.SearchString) ||
                x.PhoneNumber.Contains(request.SearchString)
            );
        }

        var shops = await query
            .OrderBy(x => x.Name)
            .Select(x => _mapper.Map<ShopDto>(x))
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result.Success(shops);
    }
}
