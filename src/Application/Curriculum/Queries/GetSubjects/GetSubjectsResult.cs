using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Application.Curriculum.Queries.GetSubjects;

public record GetSubjectsResult(List<Subject> Subjects);
