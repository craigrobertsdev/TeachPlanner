using Mapster;
using TeachPlanner.Application.Teachers.Commands.CreateTeacher;
using TeachPlanner.Application.Teachers.Common;
using TeachPlanner.Contracts.Teacher;

namespace TeachPlanner.Api.Common.Mappings;

public class TeacherMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<CreateTeacherRequest, CreateTeacherCommand>()
            .Map(dest => dest.TeacherId, src => src.TeacherId);

        config
            .NewConfig<TeacherCreatedResult, CreateTeacherResponse>();
    }
}
