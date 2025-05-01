public class CreateImportOrderCommandValidator : AbstractValidator<CreateImportOrderCommand>
{
    public CreateImportOrderCommandValidator()
    {
        RuleFor(v => v.InvoiceCode)
            .NotEmpty().WithMessage("Invoice Code is required.")
            .MaximumLength(50).WithMessage("Invoice Code must not exceed 50 characters.");

        RuleFor(v => v.ShopId)
            .NotEmpty().WithMessage("Shop ID is required.");

        RuleFor(v => v.ImportDate)
            .NotEmpty().WithMessage("Import Date is required.");

        RuleForEach(v => v.Details)
            .SetValidator(new ImportOrderDetailValidator());
    }
}

public class ImportOrderDetailValidator : AbstractValidator<ImportOrderDetailDto>
{
    public ImportOrderDetailValidator()
    {
        RuleFor(d => d.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(d => d.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        RuleFor(d => d.ImportPrice)
            .GreaterThan(0).WithMessage("Import Price must be greater than 0.");
    }
}
