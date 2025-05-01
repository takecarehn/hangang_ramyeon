using HangangRamyeon.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

public class ImportOrderCreatedEventHandler : INotificationHandler<ImportOrderCreatedEvent>
{
    private readonly ILogger<ImportOrderCreatedEventHandler> _logger;

    public ImportOrderCreatedEventHandler(ILogger<ImportOrderCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ImportOrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Import Order Created: {ImportOrderId} - {InvoiceCode}",
            notification.ImportOrder.Id, notification.ImportOrder.InvoiceCode);
        return Task.CompletedTask;
    }
}
