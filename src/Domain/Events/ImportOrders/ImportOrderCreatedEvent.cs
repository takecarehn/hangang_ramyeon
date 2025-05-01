using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Domain.Events;

public class ImportOrderCreatedEvent : BaseEvent
{
    public ImportOrderCreatedEvent(ImportOrder importOrder)
    {
        ImportOrder = importOrder;
    }

    public ImportOrder ImportOrder { get; }
}
