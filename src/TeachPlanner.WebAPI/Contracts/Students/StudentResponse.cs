using TeachPlanner.Api.Contracts.Assessments;

namespace TeachPlanner.Api.Contracts.Students;

public record StudentResponse(
    string FirstName,
    string LastName,
    List<ReportResponse> Reports,
    List<AssessmentResponse> Assessments);
