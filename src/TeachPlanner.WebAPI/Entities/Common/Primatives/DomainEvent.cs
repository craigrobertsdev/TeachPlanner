using MediatR;

namespace TeachPlanner.Api.Entities.Common.Primatives;
public record DomainEvent(Guid Id) : INotification;
