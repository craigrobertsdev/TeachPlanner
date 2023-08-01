using Application.LessonPlan.CreateLessonPlan.Commands;
using Contracts.Plannner;
using Domain.LessonPlanAggregate;
using Mapster;

namespace WebAPI.Common.Mappings;

public class LessonPlanMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<(CreateLessonPlanRequest request, string TeacherId), CreateLessonPlanCommand>()
            .Map(dest => dest, src => src.request)
            .Map(dest => dest.TeacherId, src => src.TeacherId)
            .Map(dest => dest.ResourceIds, src => src.request.ResourceIds!.Select(resourceId => resourceId).ToList())
            .Map(dest => dest.SummativeAssessmentIds, src => src.request.SummativeAssessmentIds!.Select(assessmentId => assessmentId).ToList())
            .Map(dest => dest.FormativeAssessmentIds, src => src.request.FormativeAssessmentIds!.Select(assessmentId => assessmentId).ToList());


        // not mapping comments here as this list will be null at the time of lesson creation
        config
            .NewConfig<LessonPlan, CreateLessonPlanResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.ResourceIds, src => src.ResourceIds.Select(resourceId => resourceId.Value))
            .Map(dest => dest.SummativeAssessmentIds, src => src.SummativeAssessmentIds.Select(assessmentId => assessmentId.Value))
            .Map(dest => dest.FormativeAssessmentIds, src => src.FormativeAssessmentIds.Select(assessmentId => assessmentId.Value))
            .Map(dest => dest.SubjectId, src => src.SubjectId.Value);
    }
}
