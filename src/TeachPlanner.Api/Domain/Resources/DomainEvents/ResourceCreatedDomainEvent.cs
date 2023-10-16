using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Domain.Resources.DomainEvents;

public record ResourceCreatedDomainEvent(Guid Id, ResourceId ResourceId, TeacherId TeacherId) : DomainEvent(Id);