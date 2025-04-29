using HangangRamyeon.Domain.Events.Shops;
using Microsoft.Extensions.Logging;

namespace HangangRamyeon.Application.Shops.EventHandlers;
public class ShopCompletedEventHandler : INotificationHandler<ShopCompletedEvent>
{
    private readonly ILogger<ShopCompletedEventHandler> _logger;

    public ShopCompletedEventHandler(ILogger<ShopCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ShopCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HangangRamyeon Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
