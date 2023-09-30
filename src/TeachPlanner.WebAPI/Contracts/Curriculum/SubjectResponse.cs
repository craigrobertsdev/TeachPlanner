namespace TeachPlanner.Api.Contracts.Curriculum;

public record SubjectResponse(
    Guid Id,
    string Name,
    List<YearLevelResponse> YearLevels);

public record class YearLevelResponse(
    string Name,
    string? YearLevelValue,
    string? BandLevelValue,
    string? YearLevelDescription,
    string? AchievementStandard,
    List<StrandResponse> Strands);

public record StrandResponse(
    string Name,
    List<SubstrandResponse>? Substrands,
    List<ContentDescriptionResponse>? ContentDescriptions);

public record SubstrandResponse(
    string Name,
    List<ContentDescriptionResponse> ContentDescriptions);

public record ContentDescriptionResponse(
    string Description,
    string CurriculumCode,
    List<ElaborationResponse>? Elaborations);

public record ElaborationResponse(
    string Description);
