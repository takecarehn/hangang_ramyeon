using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Domain.Entities;
using HangangRamyeon.Domain.Events.Shops;

namespace HangangRamyeon.Application.Shops.Commands.AssignUserToShop;
public record AssignUserToShopCommand : IRequest<Result>
{
    public Guid UserId { get; init; } = default!;
    public Guid ShopId { get; init; }
}

public class AssignUserToShopCommandHandler : IRequestHandler<AssignUserToShopCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public AssignUserToShopCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(AssignUserToShopCommand request, CancellationToken cancellationToken)
    {
        var shop = await _context.Shops
            .FirstOrDefaultAsync(x => x.Id == request.ShopId, cancellationToken);

        if (shop == null)
        {
            return Result.Failure("Shop not found.");
        }

        var existingAssignment = await _context.UserShops
            .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.ShopId == request.ShopId, cancellationToken);

        if (existingAssignment != null)
        {
            return Result.Failure("User is already assigned to this shop.");
        }

        var userShop = new UserShop
        {
            UserId = request.UserId,
            ShopId = request.ShopId
        };

        _context.UserShops.Add(userShop);

        shop.AddDomainEvent(new UserAssignedToShopEvent(request.UserId, request.ShopId));

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
