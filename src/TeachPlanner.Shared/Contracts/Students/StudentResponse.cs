using TeachPlanner.Shared.Contracts.Assessments;
using TeachPlanner.Shared.Contracts.Reports;

namespace TeachPlanner.Shared.Contracts.Students;

public record StudentResponse(
    string FirstName,
    string LastName,
    List<ReportResponse> Reports,
    List<AssessmentResponse> Assessments);