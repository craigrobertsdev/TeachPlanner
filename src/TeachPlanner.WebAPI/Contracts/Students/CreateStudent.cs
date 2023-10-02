using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.Api.Contracts.Students;
public record CreateStudentRequest(
    string Name,
    YearLevel YearLevel);
