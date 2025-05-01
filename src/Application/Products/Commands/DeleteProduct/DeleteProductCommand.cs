using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;

namespace HangangRamyeon.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<Result>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public DeleteProductCommandHandler(IApplicationDbContext context,
                                       IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.Id;
        if (string.IsNullOrEmpty(userId))
        {
            return Result.Failure("Unauthorized: User ID is not available.");
        }

        var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
            return Result.Failure("Product not found.");

        entity.Deleted = DateTime.UtcNow;
        entity.DeletedBy = userId;
        entity.AddDomainEvent(new ProductDeletedEvent(entity));
        _context.Products.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
