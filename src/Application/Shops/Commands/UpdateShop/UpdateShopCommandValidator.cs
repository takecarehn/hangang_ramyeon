namespace HangangRamyeon.Application.Shops.Commands.UpdateShop;
public class UpdateShopCommandValidator : AbstractValidator<UpdateShopCommand>
{
    public UpdateShopCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
        RuleFor(v => v.Code).NotEmpty().MaximumLength(50);
        RuleFor(v => v.Name).NotEmpty().MaximumLength(100);
        RuleFor(v => v.Address).NotEmpty().MaximumLength(200);
        RuleFor(v => v.PhoneNumber).NotEmpty().MaximumLength(15);
    }
}
