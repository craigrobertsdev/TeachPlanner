using TeachPlanner.Shared.Domain.Common.Primatives;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Shared.Domain.Teachers.DomainEvents;

public record TeacherCreatedDomainEvent(Guid Id, TeacherId TeacherId) : DomainEvent(Id);