using HangangRamyeon.Application.Categories.Commands.UpdateProductCategory;
using HangangRamyeon.Application.Common.Interfaces;

namespace HangangRamyeon.Application.ProductCategories.Commands.UpdateProductCategory;

public class UpdateProductCategoryCommandValidator : AbstractValidator<UpdateProductCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCategoryCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(v => v.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(50).WithMessage("Code must not exceed 50 characters.")
            .MustAsync(BeUniqueCode).WithMessage("The specified code already exists.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }

    private async Task<bool> BeUniqueCode(UpdateProductCategoryCommand command, string code, CancellationToken cancellationToken)
    {
        return await _context.ProductCategories
            .Where(x => x.Id != command.Id) // Exclude the current entity
            .AllAsync(x => x.Code != code, cancellationToken);
    }
}
