using HangangRamyeon.Domain.Events;
using Microsoft.Extensions.Logging;

namespace HangangRamyeon.Application.ProductCategories.EventHandlers;

public class ProductCategoryUpdatedEventHandler : INotificationHandler<ProductCategoryUpdatedEvent>
{
    private readonly ILogger<ProductCategoryUpdatedEventHandler> _logger;

    public ProductCategoryUpdatedEventHandler(ILogger<ProductCategoryUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductCategoryUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HangangRamyeon Domain Event: {DomainEvent}",
            notification.GetType().Name);

        return Task.CompletedTask;
    }
}
