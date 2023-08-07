using Domain.SubjectAggregates;

namespace Application.Curriculum.Queries.ListSubjects;

public record GetAllSubjectsResult(List<Subject> Subjects);
