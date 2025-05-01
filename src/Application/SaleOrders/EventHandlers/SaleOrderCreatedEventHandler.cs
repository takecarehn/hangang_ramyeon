using Microsoft.Extensions.Logging;

public class SaleOrderCreatedEventHandler : INotificationHandler<SaleOrderCreatedEvent>
{
    private readonly ILogger<SaleOrderCreatedEventHandler> _logger;

    public SaleOrderCreatedEventHandler(ILogger<SaleOrderCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleOrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SaleOrder created: {SaleOrderId}", notification.SaleOrder.Id);
        return Task.CompletedTask;
    }
}
