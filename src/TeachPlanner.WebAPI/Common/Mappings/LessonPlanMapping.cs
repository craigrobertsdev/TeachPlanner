using Mapster;
using TeachPlanner.Application.LessonPlans.CreateLessonPlan.Commands;
using TeachPlanner.Application.LessonPlans.Queries.GetLessonPlans;
using TeachPlanner.Contracts.Plannner.CreateLessonPlan;
using TeachPlanner.Contracts.Plannner.GetLessonPlans;
using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Api.Common.Mappings;

public class LessonPlanMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<(CreateLessonPlanRequest request, string TeacherId), CreateLessonPlanCommand>()
            .Map(dest => dest, src => src.request)
            .Map(dest => dest.TeacherId, src => src.TeacherId)
            .Map(dest => dest.Resources, src => src.request.Resources)
            .Map(dest => dest.SummativeAssessmentIds, src => src.request.SummativeAssessmentIds!.Select(assessmentId => assessmentId).ToList())
            .Map(dest => dest.FormativeAssessmentIds, src => src.request.FormativeAssessmentIds!.Select(assessmentId => assessmentId).ToList());


        // not mapping comments here as this list will be null at the time of lesson creation
        config
            .NewConfig<LessonPlan, CreateLessonPlanResponse>();

        config
            .NewConfig<GetLessonPlansRequest, GetLessonPlansQuery>();
    }
}
