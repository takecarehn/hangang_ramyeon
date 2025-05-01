using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;

public record GetSaleOrderDetailQuery(Guid Id) : IRequest<Result>;

public class GetSaleOrderDetailQueryHandler : IRequestHandler<GetSaleOrderDetailQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public GetSaleOrderDetailQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(GetSaleOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var saleOrder = await _context.SaleOrders
            .Include(x => x.Details)
            .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (saleOrder == null)
            return Result.Failure("SaleOrder not found.");

        var result = new
        {
            saleOrder.Id,
            saleOrder.ShopId,
            saleOrder.SaleDate,
            Details = saleOrder.Details.Select(d => new
            {
                d.ProductId,
                d.Product.Name,
                d.Quantity,
                d.SalePrice
            }).ToList()
        };

        return Result.Success(result);
    }
}
