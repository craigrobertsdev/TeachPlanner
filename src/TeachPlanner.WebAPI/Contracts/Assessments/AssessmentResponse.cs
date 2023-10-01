using TeachPlanner.Api.Entities.Assessments;
using TeachPlanner.Api.Entities.Common.Enums;

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
