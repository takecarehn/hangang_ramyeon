using HangangRamyeon.Domain.Events;
using Microsoft.Extensions.Logging;

namespace HangangRamyeon.Application.ProductCategories.EventHandlers;

public class ProductCategoryDeletedEventHandler : INotificationHandler<ProductCategoryDeletedEvent>
{
    private readonly ILogger<ProductCategoryDeletedEventHandler> _logger;

    public ProductCategoryDeletedEventHandler(ILogger<ProductCategoryDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductCategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HangangRamyeon Domain Event: {DomainEvent}",
            notification.GetType().Name);

        return Task.CompletedTask;
    }
}
