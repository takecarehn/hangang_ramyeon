using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;

namespace HangangRamyeon.Application.Shops.Commands.DeleteShop;
public record DeleteShopCommand(Guid Id) : IRequest<Result>;
public class DeleteShopCommandHandler : IRequestHandler<DeleteShopCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteShopCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteShopCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Shops
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return Result.Failure("Shop not found.");
        }

        _context.Shops.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
