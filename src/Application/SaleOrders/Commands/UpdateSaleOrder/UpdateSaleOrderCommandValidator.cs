using FluentValidation;

public class UpdateSaleOrderCommandValidator : AbstractValidator<UpdateSaleOrderCommand>
{
    public UpdateSaleOrderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.ShopId).NotEmpty();
        RuleFor(x => x.SaleDate).NotEmpty();
        RuleFor(x => x.Discount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CustomerPaid).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Details).NotEmpty();
        RuleForEach(x => x.Details).SetValidator(new SaleOrderDetailDtoValidator());
        RuleFor(x => x.Note).MaximumLength(500);
        RuleFor(x => x.ModifiedBy).MaximumLength(100);
    }
}