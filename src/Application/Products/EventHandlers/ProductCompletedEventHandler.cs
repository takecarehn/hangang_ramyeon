using HangangRamyeon.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

public class ProductCompletedEventHandler : INotificationHandler<ProductCompletedEvent>
{
    private readonly ILogger<ProductCompletedEventHandler> _logger;

    public ProductCompletedEventHandler(ILogger<ProductCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Product completed: {ProductId} - {ProductName}", notification.Product.Id, notification.Product.Name);
        return Task.CompletedTask;
    }
}