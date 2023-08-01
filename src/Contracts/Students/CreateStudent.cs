using Domain.SubjectAggregates;

namespace Contracts.Students;
public record CreateStudentRequest(
    string Name,
    YearLevel YearLevel);
