using HangangRamyeon.Domain.Events;
using Microsoft.Extensions.Logging;

namespace HangangRamyeon.Application.ProductCategories.EventHandlers;

public class ProductCategoryCompletedEventHandler : INotificationHandler<ProductCategoryCompletedEvent>
{
    private readonly ILogger<ProductCategoryCompletedEventHandler> _logger;

    public ProductCategoryCompletedEventHandler(ILogger<ProductCategoryCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductCategoryCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HangangRamyeon Domain Event: {DomainEvent}",
            notification.GetType().Name);

        return Task.CompletedTask;
    }
}
