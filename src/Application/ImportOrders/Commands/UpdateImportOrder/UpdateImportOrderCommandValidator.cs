public class UpdateImportOrderCommandValidator : AbstractValidator<UpdateImportOrderCommand>
{
    public UpdateImportOrderCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Import Order ID is required.");

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
