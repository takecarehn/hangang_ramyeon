namespace HangangRamyeon.Application.Shops.Commands.RemoveUserFromShop;
public class RemoveUserFromShopCommandValidator : AbstractValidator<RemoveUserFromShopCommand>
{
    public RemoveUserFromShopCommandValidator()
    {
        RuleFor(v => v.UserId).NotEmpty();
        RuleFor(v => v.ShopId).NotEmpty();
    }
}
