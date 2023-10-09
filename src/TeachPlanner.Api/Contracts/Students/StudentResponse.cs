using TeachPlanner.Api.Contracts.Assessments;
using TeachPlanner.Api.Contracts.Reports;

namespace TeachPlanner.Api.Contracts.Students;

public record StudentResponse(
    string FirstName,
    string LastName,
    List<ReportResponse> Reports,
    List<AssessmentResponse> Assessments);
