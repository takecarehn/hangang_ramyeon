namespace HangangRamyeon.Application.Shops.Commands.CreateShop;
public class CreateShopCommandValidator : AbstractValidator<CreateShopCommand>
{
    public CreateShopCommandValidator()
    {
        RuleFor(v => v.Code).NotEmpty().MaximumLength(50);
        RuleFor(v => v.Name).NotEmpty().MaximumLength(100);
        RuleFor(v => v.Address).NotEmpty().MaximumLength(200);
        RuleFor(v => v.PhoneNumber).NotEmpty().MaximumLength(15);
    }
}
