public class CreateSaleOrderCommandValidator : AbstractValidator<CreateSaleOrderCommand>
{
    public CreateSaleOrderCommandValidator()
    {
        RuleFor(x => x.InvoiceCode).NotEmpty();
        RuleFor(x => x.ShopId).NotEmpty();
        RuleFor(x => x.SaleDate).NotEmpty();
        RuleFor(x => x.Discount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CustomerPaid).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Details).NotEmpty();
        RuleForEach(x => x.Details).SetValidator(new SaleOrderDetailDtoValidator());
        RuleFor(x => x.Note).MaximumLength(500);
    }
}

public class SaleOrderDetailDtoValidator : AbstractValidator<SaleOrderDetailDto>
{
    public SaleOrderDetailDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.SalePrice).GreaterThanOrEqualTo(0);
    }
}
