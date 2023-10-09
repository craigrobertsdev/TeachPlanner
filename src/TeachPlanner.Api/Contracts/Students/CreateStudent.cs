using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Contracts.Students;
public record CreateStudentRequest(
    string Name,
    YearLevel YearLevel);
