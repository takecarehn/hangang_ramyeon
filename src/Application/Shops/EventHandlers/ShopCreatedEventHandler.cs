using HangangRamyeon.Domain.Events.Shops;
using Microsoft.Extensions.Logging;

namespace HangangRamyeon.Application.Shops.EventHandlers;
public class ShopCreatedEventHandler : INotificationHandler<ShopCreatedEvent>
{
    private readonly ILogger<ShopCreatedEventHandler> _logger;

    public ShopCreatedEventHandler(ILogger<ShopCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ShopCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HangangRamyeon Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
