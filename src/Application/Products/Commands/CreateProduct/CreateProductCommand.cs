using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<Result>
{
    public string Code { get; init; } = default!;
    public string Name { get; init; } = default!;
    public Guid CategoryId { get; init; }
    public string? Description { get; init; }
    public string? Supplier { get; init; }
    public decimal Weight { get; init; }
    public string Unit { get; init; } = default!;
    public DateTime ManufactureDate { get; init; }
    public DateTime ExpiryDate { get; init; }
    public decimal ImportPrice { get; init; }
    public decimal SalePrice { get; init; }
    public Guid ShopId { get; init; }
    public List<string> Images { get; init; } = new();
    public List<ProductAttributeDto> Attributes { get; init; } = new();
}

public record ProductAttributeDto
{
    public string Name { get; init; } = default!;
    public string Value { get; init; } = default!;
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var entity = new Product
            {
                Code = request.Code,
                Name = request.Name,
                CategoryId = request.CategoryId,
                Description = request.Description,
                Supplier = request.Supplier,
                Weight = request.Weight,
                Unit = request.Unit,
                ManufactureDate = request.ManufactureDate,
                ExpiryDate = request.ExpiryDate,
                ImportPrice = request.ImportPrice,
                SalePrice = request.SalePrice,
                ShopId = request.ShopId
            };

            // Add Images
            foreach (var imageUrl in request.Images)
            {
                entity.Images.Add(new ProductImage
                {
                    ImageUrl = imageUrl
                });
            }

            // Add Attributes
            foreach (var attribute in request.Attributes)
            {
                entity.Attributes.Add(new ProductAttribute
                {
                    Name = attribute.Name,
                    Value = attribute.Value
                });
            }

            entity.AddDomainEvent(new ProductCreatedEvent(entity));
            _context.Products.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            // Rollback transaction
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure($"An error occurred: {ex.Message}");
        }
    }
}
