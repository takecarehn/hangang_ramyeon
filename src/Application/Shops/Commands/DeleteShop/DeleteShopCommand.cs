using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;

namespace HangangRamyeon.Application.Shops.Commands.DeleteShop;
public record DeleteShopCommand(Guid Id) : IRequest<Result>;
public class DeleteShopCommandHandler : IRequestHandler<DeleteShopCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public DeleteShopCommandHandler(IApplicationDbContext context,
                                    IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(DeleteShopCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.Id;
        if (string.IsNullOrEmpty(userId))
        {
            return Result.Failure("Unauthorized: User ID is not available.");
        }

        var entity = await _context.Shops
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return Result.Failure("Shop not found.");
        }

        entity.DeletedBy = userId;
        entity.Deleted = DateTime.UtcNow;
        _context.Shops.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
