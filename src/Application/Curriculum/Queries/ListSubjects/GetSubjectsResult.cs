using Domain.SubjectAggregates;

namespace Application.Curriculum.Queries.ListSubjects;

public record GetSubjectsResult(List<Subject> Subjects);
