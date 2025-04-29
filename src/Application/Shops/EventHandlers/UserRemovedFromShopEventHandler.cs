using HangangRamyeon.Domain.Events.Shops;
using Microsoft.Extensions.Logging;

namespace HangangRamyeon.Application.Shops.EventHandlers;
public class UserRemovedFromShopEventHandler : INotificationHandler<UserRemovedFromShopEvent>
{
    private readonly ILogger<UserRemovedFromShopEventHandler> _logger;

    public UserRemovedFromShopEventHandler(ILogger<UserRemovedFromShopEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserRemovedFromShopEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HangangRamyeon Domain Event: {DomainEvent} - User {UserId} removed from Shop {ShopId}",
            notification.GetType().Name, notification.UserId, notification.ShopId);

        return Task.CompletedTask;
    }
}
