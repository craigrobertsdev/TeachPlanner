using MediatR;
using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Application.YearDataRecords;
internal sealed class YearDataCreatedDomainEventHandler : INotificationHandler<YearDataCreatedDomainEvent>
{

    public YearDataCreatedDomainEventHandler()
    {
    }
    public async Task Handle(YearDataCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
    }
}
