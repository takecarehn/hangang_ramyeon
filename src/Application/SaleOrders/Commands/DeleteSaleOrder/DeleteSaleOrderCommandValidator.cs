public class DeleteSaleOrderCommandValidator : AbstractValidator<DeleteSaleOrderCommand>
{
    public DeleteSaleOrderCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }
}
