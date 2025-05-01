using HangangRamyeon.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Product created: {ProductId} - {ProductName}", notification.Product.Id, notification.Product.Name);
        return Task.CompletedTask;
    }
}