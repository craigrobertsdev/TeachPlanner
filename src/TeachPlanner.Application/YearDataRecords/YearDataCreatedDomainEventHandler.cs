using MediatR;
using Rebus.Bus;
using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Application.YearDataRecords;
internal sealed class YearDataCreatedDomainEventHandler : INotificationHandler<YearDataCreatedDomainEvent>
{
    private readonly IBus _bus;

    public YearDataCreatedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }
    public async Task Handle(YearDataCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Send(new YearDataCreatedEvent(notification.YearDataId));
    }
}
