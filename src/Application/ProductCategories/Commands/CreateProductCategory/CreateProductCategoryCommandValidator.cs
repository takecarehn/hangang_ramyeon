using HangangRamyeon.Application.Common.Interfaces;

namespace HangangRamyeon.Application.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCategoryCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(50).WithMessage("Code must not exceed 50 characters.")
            .MustAsync(BeUniqueCode).WithMessage("The specified code already exists.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }

    private async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken)
    {
        return await _context.ProductCategories
            .AllAsync(l => l.Code != code, cancellationToken);
    }
}
