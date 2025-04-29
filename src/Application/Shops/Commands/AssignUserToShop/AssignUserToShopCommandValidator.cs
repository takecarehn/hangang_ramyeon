namespace HangangRamyeon.Application.Shops.Commands.AssignUserToShop;
public class AssignUserToShopCommandValidator : AbstractValidator<AssignUserToShopCommand>
{
    public AssignUserToShopCommandValidator()
    {
        RuleFor(v => v.UserId).NotEmpty();
        RuleFor(v => v.ShopId).NotEmpty();
    }
}
