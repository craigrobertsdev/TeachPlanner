using Mapster;
using TeachPlanner.Application.Curriculum.Queries.GetAllSubjects;
using TeachPlanner.Contracts.Curriculum;
using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Api.Common.Mappings;

public class SubjectMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Subject, SubjectResponse>()
            .Map(dest => dest.YearLevels, src => src.YearLevels.Select(yearLevel => yearLevel).ToList());

        config
            .NewConfig<YearLevel, YearLevelResponse>()
            .Map(dest => dest.YearLevelValue, src => src.YearLevelValue.ToString(), src => src.YearLevelValue != null)
            .Map(dest => dest.BandLevelValue, src => src.BandLevelValue.ToString(), src => src.BandLevelValue != null)
            .Map(dest => dest.YearLevelDescription, src => src.YearLevelDescription, src => src.YearLevelDescription != null)
            .Map(dest => dest.AchievementStandard, src => src.AchievementStandard, src => src.AchievementStandard != null)
            .Map(dest => dest.Strands, src => src.Strands.Select(strand => strand).ToList());

        config
            .NewConfig<Strand, StrandResponse>()
            .Map(dest => dest.Substrands, src => src.Substrands, src => src.Substrands != null)
            .Map(dest => dest.ContentDescriptions, src => src.ContentDescriptions, src => src.ContentDescriptions != null);

        config
            .NewConfig<Substrand, SubstrandResponse>()
            .Map(dest => dest.ContentDescriptions, src => src.ContentDescriptions.Select(contentDescription => contentDescription).ToList());

        config
            .NewConfig<ContentDescription, ContentDescriptionResponse>()
            .Map(dest => dest.Elaborations, src => src.Elaborations.Select(elaboration => elaboration).ToList());

        config
            .NewConfig<GetAllSubjectsResult, GetSubjectsResponse>()
            .Map(dest => dest.Subjects, src => src.Subjects.Select(subject => subject.Adapt<SubjectResponse>()).ToList());
    }
}
