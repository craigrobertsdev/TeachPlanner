using MediatR;
using TeachPlanner.Api.Domain.Common.Primatives;

namespace TeachPlanner.Api.Domain.Teachers.DomainEvents;

public record TeacherCreatedDomainEvent(Guid Id, TeacherId TeacherId) : DomainEvent(Id);
