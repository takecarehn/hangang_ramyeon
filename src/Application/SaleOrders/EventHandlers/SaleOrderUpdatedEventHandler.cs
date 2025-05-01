using Microsoft.Extensions.Logging;

public class SaleOrderUpdatedEventHandler : INotificationHandler<SaleOrderUpdatedEvent>
{
    private readonly ILogger<SaleOrderUpdatedEventHandler> _logger;

    public SaleOrderUpdatedEventHandler(ILogger<SaleOrderUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleOrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SaleOrder updated: {SaleOrderId} - {InvoiceCode}", notification.SaleOrder.Id, notification.SaleOrder.InvoiceCode);
        return Task.CompletedTask;
    }
}
