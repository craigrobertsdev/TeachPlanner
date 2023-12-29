using MediatR;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.Common.Primatives;

public record DomainEvent(Guid Id) : INotification, IDomainEvent;