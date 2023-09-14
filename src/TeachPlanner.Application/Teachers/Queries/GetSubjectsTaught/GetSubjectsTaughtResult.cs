using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Application.Teachers.Queries.GetSubjectsTaught;
public record GetSubjectsTaughtResult(
    List<Subject> Subjects);
