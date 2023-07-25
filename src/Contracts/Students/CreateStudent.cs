using Domain.SubjectAggregates.Entities;

namespace Contracts.Students;
public record CreateStudentRequest(
    string Name,
    YearLevel YearLevel);
