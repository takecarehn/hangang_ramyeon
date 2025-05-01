using FluentValidation;
using HangangRamyeon.Application.Products.Commands.UpdateProduct;
using HangangRamyeon.Application.Common.Interfaces;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Id)
            .NotEmpty();

        RuleFor(v => v.Code)
            .NotEmpty().MaximumLength(50);

        RuleFor(v => v.Name)
            .NotEmpty().MaximumLength(100);

        RuleFor(v => v.CategoryId)
            .NotEmpty();
    }
}