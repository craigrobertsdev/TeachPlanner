using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Application.Teachers.Commands.SetSubjectsTaught;
public record SetSubjectsTaughtResult(List<Subject> Subjects);
