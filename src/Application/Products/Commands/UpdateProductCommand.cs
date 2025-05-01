using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Application.Products.Commands.CreateProduct;
using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<Result>
{
    public Guid Id { get; init; }
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

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {

            var entity = await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Attributes)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                return Result.Failure("Product not found.");

            entity.Code = request.Code;
            entity.Name = request.Name;
            entity.CategoryId = request.CategoryId;
            entity.Description = request.Description;
            entity.Supplier = request.Supplier;
            entity.Weight = request.Weight;
            entity.Unit = request.Unit;
            entity.ManufactureDate = request.ManufactureDate;
            entity.ExpiryDate = request.ExpiryDate;
            entity.ImportPrice = request.ImportPrice;
            entity.SalePrice = request.SalePrice;
            entity.ShopId = request.ShopId;

            // Remove old images and add new ones
            _context.ProductImages.RemoveRange(entity.Images);
            foreach (var imageUrl in request.Images)
            {
                entity.Images.Add(new ProductImage
                {
                    ImageUrl = imageUrl
                });
            }

            // Remove old attributes and add new ones
            _context.ProductAttributes.RemoveRange(entity.Attributes);
            foreach (var attribute in request.Attributes)
            {
                entity.Attributes.Add(new ProductAttribute
                {
                    Name = attribute.Name,
                    Value = attribute.Value
                });
            }

            entity.AddDomainEvent(new ProductUpdatedEvent(entity));
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            // Rollback transaction
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure($"An error occurred: {ex.Message}");
        }
    }
}
