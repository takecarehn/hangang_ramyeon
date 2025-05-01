using HangangRamyeon.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

public class ImportOrderUpdatedEventHandler : INotificationHandler<ImportOrderUpdatedEvent>
{
    private readonly ILogger<ImportOrderUpdatedEventHandler> _logger;

    public ImportOrderUpdatedEventHandler(ILogger<ImportOrderUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ImportOrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Import Order Updated: {ImportOrderId} - {InvoiceCode}",
            notification.ImportOrder.Id, notification.ImportOrder.InvoiceCode);
        return Task.CompletedTask;
    }
}
