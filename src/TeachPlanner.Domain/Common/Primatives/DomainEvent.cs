using MediatR;

namespace TeachPlanner.Domain.Common.Primatives;
public record DomainEvent(Guid Id) : INotification;
