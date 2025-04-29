using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Domain.Events.Shops;

namespace HangangRamyeon.Application.Shops.Commands.RemoveUserFromShop;
public record RemoveUserFromShopCommand : IRequest<Result>
{
    public Guid UserId { get; init; } = default!;
    public Guid ShopId { get; init; }
}

public class RemoveUserFromShopCommandHandler : IRequestHandler<RemoveUserFromShopCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public RemoveUserFromShopCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(RemoveUserFromShopCommand request, CancellationToken cancellationToken)
    {
        var userShop = await _context.UserShops
            .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.ShopId == request.ShopId, cancellationToken);

        if (userShop == null)
        {
            return Result.Failure("User is not assigned to this shop.");
        }

        _context.UserShops.Remove(userShop);

        var shop = await _context.Shops.FindAsync(request.ShopId);
        if (shop != null)
        {
            shop.AddDomainEvent(new UserRemovedFromShopEvent(request.UserId, request.ShopId));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
