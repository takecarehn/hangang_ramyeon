using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Mappings;
using HangangRamyeon.Application.Common.Models;
public record GetSaleOrdersQuery(int PageNumber = 1, int PageSize = 10, Guid? ShopId = null) : IRequest<Result>;

public class GetSaleOrdersQueryHandler : IRequestHandler<GetSaleOrdersQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public GetSaleOrdersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(GetSaleOrdersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.SaleOrders.AsQueryable();
        if (request.ShopId.HasValue)
            query = query.Where(x => x.ShopId == request.ShopId);

        var paged = await query
            .OrderByDescending(x => x.SaleDate)
            .Select(x => new
            {
                x.Id,
                x.ShopId,
                x.SaleDate
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result.Success(paged);
    }
}
