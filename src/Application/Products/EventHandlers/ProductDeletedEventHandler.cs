using HangangRamyeon.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

public class ProductDeletedEventHandler : INotificationHandler<ProductDeletedEvent>
{
    private readonly ILogger<ProductDeletedEventHandler> _logger;

    public ProductDeletedEventHandler(ILogger<ProductDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Product deleted: {ProductId} - {ProductName}", notification.Product.Id, notification.Product.Name);
        return Task.CompletedTask;
    }
}