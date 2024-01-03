using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Shared.Contracts.Teachers;

public record TeacherResponse(
    TeacherId Id,
    string FirstName,
    string LastName);