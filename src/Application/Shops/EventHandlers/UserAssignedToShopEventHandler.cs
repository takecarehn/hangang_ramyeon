using HangangRamyeon.Domain.Events.Shops;
using Microsoft.Extensions.Logging;

namespace HangangRamyeon.Application.Shops.EventHandlers;
public class UserAssignedToShopEventHandler : INotificationHandler<UserAssignedToShopEvent>
{
    private readonly ILogger<UserAssignedToShopEventHandler> _logger;

    public UserAssignedToShopEventHandler(ILogger<UserAssignedToShopEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserAssignedToShopEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HangangRamyeon Domain Event: {DomainEvent} - User {UserId} assigned to Shop {ShopId}",
            notification.GetType().Name, notification.UserId, notification.ShopId);

        return Task.CompletedTask;
    }
}
