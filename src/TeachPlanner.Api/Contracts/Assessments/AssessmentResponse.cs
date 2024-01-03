using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Shared.Domain.Common.Enums;

namespace TeachPlanner.Api.Contracts.Assessments;

public record AssessmentResponse(
    Guid SubjectId,
    Guid StudentId,
    YearLevelValue YearLevel,
    AssessmentResultResponse AssessmentResult,
    string PlanningNotes,
    DateTime ConductedDateTime);

public record AssessmentResultResponse(
    string Comments,
    AssessmentGrade Grade,
    DateTime DateMarked);