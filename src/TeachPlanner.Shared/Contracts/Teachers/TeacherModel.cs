using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Shared.Contracts.Teachers;

public record TeacherModel(
    TeacherId Id,
    string FirstName,
    string LastName);