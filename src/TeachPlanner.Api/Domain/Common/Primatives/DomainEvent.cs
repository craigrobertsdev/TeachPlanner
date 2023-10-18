using MediatR;
using TeachPlanner.Api.Domain.Common.Interfaces;

namespace TeachPlanner.Api.Domain.Common.Primatives;

public record DomainEvent(Guid Id) : INotification, IDomainEvent;