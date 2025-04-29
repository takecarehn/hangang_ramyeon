using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Domain.Entities;
using HangangRamyeon.Domain.Events.Shops;

namespace HangangRamyeon.Application.Shops.Commands.CreateShop;
public record CreateShopCommand : IRequest<Result>
{
    public string Code { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Address { get; init; } = default!;
    public string PhoneNumber { get; init; } = default!;
    public string? TaxCode { get; init; }
}
public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateShopCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateShopCommand request, CancellationToken cancellationToken)
    {
        var entity = new Shop
        {
            Code = request.Code,
            Name = request.Name,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            TaxCode = request.TaxCode
        };

        entity.AddDomainEvent(new ShopCreatedEvent(entity));
        _context.Shops.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(entity.Id);
    }
}

