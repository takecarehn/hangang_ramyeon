using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Domain.Events;

namespace HangangRamyeon.Application.Categories.Commands.DeleteProductCategory;

public record DeleteProductCategoryCommand(Guid Id) : IRequest<Result>;

public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public DeleteProductCategoryCommandHandler(IApplicationDbContext context,
                                               IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.Id;

        if (string.IsNullOrEmpty(userId))
        {
            return Result.Failure("Unauthorized: User ID is not available.");
        }

        var entity = await _context.ProductCategories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return Result.Failure("ProductCategory not found.");
        }

        entity.AddDomainEvent(new ProductCategoryDeletedEvent(entity));

        entity.DeletedBy = userId;
        entity.Deleted = DateTime.UtcNow;
        _context.ProductCategories.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
