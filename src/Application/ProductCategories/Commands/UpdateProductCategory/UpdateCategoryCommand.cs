using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Domain.Events;

namespace HangangRamyeon.Application.Categories.Commands.UpdateProductCategory;

public record UpdateProductCategoryCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string Code { get; init; } = default!;
    public string Name { get; init; } = default!;
}

public class UpdateProductCategoryCommandHandler : IRequestHandler<UpdateProductCategoryCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductCategories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return Result.Failure("ProductCategory not found.");
        }

        entity.Code = request.Code;
        entity.Name = request.Name;

        entity.AddDomainEvent(new ProductCategoryUpdatedEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
