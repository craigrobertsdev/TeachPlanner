using Domain.Common.Curriculum.Entities;

namespace Contracts.Students;
public record CreateStudentRequest(
    string Name,
    YearLevel YearLevel);
