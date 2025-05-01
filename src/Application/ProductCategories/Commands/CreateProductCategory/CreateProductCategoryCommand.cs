using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Domain.Entities;
using HangangRamyeon.Domain.Events;

namespace HangangRamyeon.Application.ProductCategories.Commands.CreateProductCategory;

public record CreateProductCategoryCommand : IRequest<Result>
{
    public string Code { get; init; } = default!;
    public string Name { get; init; } = default!;
}

public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProductCategory
        {
            Code = request.Code,
            Name = request.Name
        };

        entity.AddDomainEvent(new ProductCategoryCreatedEvent(entity));
        _context.ProductCategories.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(entity.Id);
    }
}
