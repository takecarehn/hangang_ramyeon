

using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;

namespace HangangRamyeon.Application.Shops.Commands.UpdateShop;
public record UpdateShopCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string Code { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Address { get; init; } = default!;
    public string PhoneNumber { get; init; } = default!;
    public string? TaxCode { get; init; }
}

public class UpdateShopCommandHandler : IRequestHandler<UpdateShopCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateShopCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Shops
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return Result.Failure("Shop not found.");
        }

        entity.Code = request.Code;
        entity.Name = request.Name;
        entity.Address = request.Address;
        entity.PhoneNumber = request.PhoneNumber;
        entity.TaxCode = request.TaxCode;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
