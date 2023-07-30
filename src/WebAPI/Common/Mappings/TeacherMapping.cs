using Application.Teachers.Common;
using Application.Teachers.CreateTeacher.Commands;
using Contracts.Teacher;
using Mapster;

namespace WebAPI.Common.Mappings;

public class TeacherMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<CreateTeacherRequest, CreateTeacherCommand>()
            .Map(dest => dest.UserId, src => src.UserId);

        config
            .NewConfig<TeacherCreatedResult, CreateTeacherResponse>()
            .Map(dest => dest.TeacherId, src => src.Teacher.Id.Value);
    }
}
