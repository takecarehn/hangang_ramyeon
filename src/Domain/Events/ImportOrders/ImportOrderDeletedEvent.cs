using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Domain.Events;

public class ImportOrderDeletedEvent : BaseEvent
{
    public ImportOrderDeletedEvent(ImportOrder importOrder)
    {
        ImportOrder = importOrder;
    }

    public ImportOrder ImportOrder { get; }
}
