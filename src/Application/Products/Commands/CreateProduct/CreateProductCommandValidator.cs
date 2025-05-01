using FluentValidation;
using HangangRamyeon.Application.Products.Commands.CreateProduct;
using HangangRamyeon.Application.Common.Interfaces;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(50)
            .MustAsync(BeUniqueCode).WithMessage("Code already exists.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(v => v.CategoryId)
            .NotEmpty().WithMessage("Category is required.");
    }

    private async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken)
    {
        return await _context.Products.AllAsync(x => x.Code != code, cancellationToken);
    }
}