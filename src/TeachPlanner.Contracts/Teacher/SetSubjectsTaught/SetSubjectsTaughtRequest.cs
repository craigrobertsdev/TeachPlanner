namespace TeachPlanner.Contracts.Teacher.SetSubjectsTaught;
public record SetSubjectsTaughtRequest(string TeacherId, List<string> SubjectNames);
