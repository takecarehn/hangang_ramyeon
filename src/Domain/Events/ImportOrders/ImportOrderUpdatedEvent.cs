using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Domain.Events;

public class ImportOrderUpdatedEvent : BaseEvent
{
    public ImportOrderUpdatedEvent(ImportOrder importOrder)
    {
        ImportOrder = importOrder;
    }

    public ImportOrder ImportOrder { get; }
}
