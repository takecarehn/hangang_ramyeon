using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;

namespace HangangRamyeon.Application.Products.Queries.GetProductDetail;

public record GetProductDetailQuery(Guid Id) : IRequest<Result>;

public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Attributes)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product == null)
            return Result.Failure("Product not found.");

        return Result.Success(_mapper.Map<ProductDto>(product));
    }
}
