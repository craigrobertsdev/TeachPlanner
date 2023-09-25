using Mapster;
using TeachPlanner.Application.Teachers.Common;
using TeachPlanner.Contracts.Teacher.CreateTeacher;

namespace TeachPlanner.Api.Common.Mappings;

public class TeacherMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<TeacherCreatedResult, CreateTeacherResponse>();
    }
}
