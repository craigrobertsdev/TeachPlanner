﻿using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Contracts.Subjects;

public record SubjectResponse(
    Guid Id,
    string Name,
    List<YearLevelResponse> YearLevels) {
    public static List<SubjectResponse> CreateCurriculumSubjectResponses(IEnumerable<CurriculumSubject> subjects,
        bool withDetails) {
        List<SubjectResponse> subjectResponses = new();
        subjectResponses = subjects.Select(s => new SubjectResponse(
            s.Id.Value,
            s.Name,
            CreateYearLevelResponses(s.YearLevels, withDetails))).ToList();

        return subjectResponses;
    }

    private static List<YearLevelResponse> CreateYearLevelResponses(IEnumerable<YearLevel> yearLevels, bool withDetails) {
        List<YearLevelResponse> yearLevelResponses = new();
        yearLevelResponses = yearLevels.Select(yl => new YearLevelResponse(
            yl.Name,
            yl.YearLevelValue.ToString(),
            yl.BandLevelValue.ToString(),
            withDetails ? yl.YearLevelDescription : null,
            withDetails ? yl.AchievementStandard : null,
            CreateStrandResponses(yl.Strands, withDetails))).ToList();

        return yearLevelResponses;
    }

    private static List<StrandResponse> CreateStrandResponses(IEnumerable<Strand> strands, bool withDetails) {
        List<StrandResponse> strandResponses = new();
        strandResponses = strands.Select(s => new StrandResponse(
            s.Name,
            s.ContentDescriptions != null
                ? CreateContentDescriptionResponses(s.ContentDescriptions, withDetails)
                : null)).ToList();

        return strandResponses;
    }

    private static List<ContentDescriptionResponse> CreateContentDescriptionResponses(
        IEnumerable<ContentDescription> contentDescriptions, bool withDetails) {
        List<ContentDescriptionResponse> contentDescriptionResponses = new();
        contentDescriptionResponses = contentDescriptions.Select(cd => new ContentDescriptionResponse(
            cd.Description,
            cd.CurriculumCode,
            cd.Substrand,
            withDetails ? CreateElaborationResponses(cd.Elaborations) : null)).ToList();

        return contentDescriptionResponses;
    }

    private static List<ElaborationResponse> CreateElaborationResponses(IEnumerable<Elaboration> elaborations) {
        List<ElaborationResponse> elaborationResponses = new();
        elaborationResponses = elaborations.Select(e => new ElaborationResponse(e.Description)).ToList();

        return elaborationResponses;
    }
}

public record class YearLevelResponse(
    string Name,
    string? YearLevelValue,
    string? BandLevelValue,
    string? YearLevelDescription,
    string? AchievementStandard,
    List<StrandResponse> Strands);

public record StrandResponse(
    string Name,
    List<ContentDescriptionResponse>? ContentDescriptions);

public record ContentDescriptionResponse(
    string Description,
    string CurriculumCode,
    string Substrand,
    List<ElaborationResponse>? Elaborations);

public record ElaborationResponse(
    string Description);