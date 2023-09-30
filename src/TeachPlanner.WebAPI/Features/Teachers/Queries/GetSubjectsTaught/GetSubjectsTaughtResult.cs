using TeachPlanner.Api.Entities.Subjects;

namespace TeachPlanner.Api.Features.Teachers.Queries.GetSubjectsTaught;
public record GetSubjectsTaughtResult(
    List<Subject> Subjects);
