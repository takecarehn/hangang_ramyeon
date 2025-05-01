using Microsoft.Extensions.Logging;

public class SaleOrderDeletedEventHandler : INotificationHandler<SaleOrderDeletedEvent>
{
    private readonly ILogger<SaleOrderDeletedEventHandler> _logger;

    public SaleOrderDeletedEventHandler(ILogger<SaleOrderDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleOrderDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SaleOrder deleted: {SaleOrderId} - {InvoiceCode}", notification.SaleOrder.Id, notification.SaleOrder.InvoiceCode);
        return Task.CompletedTask;
    }
}
