using HangangRamyeon.Domain.Events;
using Microsoft.Extensions.Logging;

public class ImportOrderDeletedEventHandler : INotificationHandler<ImportOrderDeletedEvent>
{
    private readonly ILogger<ImportOrderDeletedEventHandler> _logger;

    public ImportOrderDeletedEventHandler(ILogger<ImportOrderDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ImportOrderDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Import Order Deleted: {ImportOrderId} - {InvoiceCode}",
            notification.ImportOrder.Id, notification.ImportOrder.InvoiceCode);
        return Task.CompletedTask;
    }
}
