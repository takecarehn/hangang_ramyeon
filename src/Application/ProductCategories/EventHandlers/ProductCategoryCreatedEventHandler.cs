using HangangRamyeon.Domain.Events;
using Microsoft.Extensions.Logging;

namespace HangangRamyeon.Application.ProductCategories.EventHandlers;

public class ProductCategoryCreatedEventHandler : INotificationHandler<ProductCategoryCreatedEvent>
{
    private readonly ILogger<ProductCategoryCreatedEventHandler> _logger;

    public ProductCategoryCreatedEventHandler(ILogger<ProductCategoryCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductCategoryCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HangangRamyeon Domain Event: {DomainEvent}",
            notification.GetType().Name);

        return Task.CompletedTask;
    }
}
