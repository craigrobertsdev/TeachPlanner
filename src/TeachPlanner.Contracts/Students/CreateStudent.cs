using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Contracts.Students;
public record CreateStudentRequest(
    string Name,
    YearLevel YearLevel);
