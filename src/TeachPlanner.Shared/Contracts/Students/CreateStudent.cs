using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Shared.Contracts.Students;

public record CreateStudentRequest(
    string Name,
    YearLevel YearLevel);