using Microsoft.Extensions.Logging;

public class SaleOrderCompletedEventHandler : INotificationHandler<SaleOrderCompletedEvent>
{
    private readonly ILogger<SaleOrderCompletedEventHandler> _logger;

    public SaleOrderCompletedEventHandler(ILogger<SaleOrderCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleOrderCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SaleOrder Completed: {SaleOrderId}", notification.SaleOrder.Id);
        return Task.CompletedTask;
    }
}
