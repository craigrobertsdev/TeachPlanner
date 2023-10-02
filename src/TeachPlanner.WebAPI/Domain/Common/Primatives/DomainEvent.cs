using MediatR;

namespace TeachPlanner.Api.Domain.Common.Primatives;
public record DomainEvent(Guid Id) : INotification;
