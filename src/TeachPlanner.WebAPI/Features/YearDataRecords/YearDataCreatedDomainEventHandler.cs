using MediatR;
using TeachPlanner.Api.Entities.YearDataRecords;

namespace TeachPlanner.Api.Features.YearDataRecords;
internal sealed class YearDataCreatedDomainEventHandler : INotificationHandler<YearDataCreatedDomainEvent>
{

    public YearDataCreatedDomainEventHandler()
    {
    }
    public async Task Handle(YearDataCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
    }
}
