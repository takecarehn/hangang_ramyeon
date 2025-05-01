using HangangRamyeon.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

public class ProductUpdatedEventHandler : INotificationHandler<ProductUpdatedEvent>
{
    private readonly ILogger<ProductUpdatedEventHandler> _logger;

    public ProductUpdatedEventHandler(ILogger<ProductUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Product updated: {ProductId} - {ProductName}", notification.Product.Id, notification.Product.Name);
        return Task.CompletedTask;
    }
}