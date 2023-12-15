namespace TeachPlanner.Api.Contracts.Curriculum;

public record CurriculumSubjectDto(
    string Name,
    List<YearLevelDto> YearLevels);

public record YearLevelDto(
    string YearLevel,
    List<ContentDescriptionDto> ContentDescriptions);

public record ContentDescriptionDto(
    string Strand,
    string CurriculumCode,
    string ContentDescription);
