using Application.LessonPlan.CreateLessonPlan.Commands;
using Contracts.Plannner;
using Mapster;

namespace WebAPI.Common.Mappings;

public class LessonPlanMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<(CreateLessonPlanRequest request, string TeacherId), CreateLessonPlanCommand>()
            .Map(dest => dest, src => src.request)
            .Map(dest => dest.TeacherId, src => src.TeacherId);

    }
}
