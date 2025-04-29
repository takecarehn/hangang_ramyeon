using HangangRamyeon.Domain.Events.Shops;
using Microsoft.Extensions.Logging;

namespace HangangRamyeon.Application.Shops.EventHandlers;
public class ShopUpdatedEventHandler : INotificationHandler<ShopUpdatedEvent>
{
    private readonly ILogger<ShopUpdatedEventHandler> _logger;

    public ShopUpdatedEventHandler(ILogger<ShopUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ShopUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HangangRamyeon Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
