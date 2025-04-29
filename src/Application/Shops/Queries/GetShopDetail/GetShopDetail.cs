using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;

namespace HangangRamyeon.Application.Shops.Queries.GetShopDetail;
public record GetShopDetailQuery(Guid Id) : IRequest<Result>;

public class GetShopDetailQueryHandler : IRequestHandler<GetShopDetailQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShopDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result> Handle(GetShopDetailQuery request, CancellationToken cancellationToken)
    {
        var shop = await _context.Shops
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (shop == null)
        {
            return Result.Failure("Shop not found!");
        }

        return Result.Success(_mapper.Map<ShopDto>(shop));
    }
}
