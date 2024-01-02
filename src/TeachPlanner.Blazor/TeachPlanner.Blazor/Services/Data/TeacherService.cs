using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Common.Interfaces.Services;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Blazor.Services.Data;

public class TeacherService : ITeacherService {
    private readonly ITeacherRepository _teacherRepository;

    public TeacherService(ITeacherRepository teacherRepository) {
        _teacherRepository = teacherRepository;
    }

    public async Task<Guid?> GetTeacherId(string userId, CancellationToken cancellationToken) {
        Console.WriteLine("Calling GetTeacherId from Server");
        var teacher = await _teacherRepository.GetByUserId(userId, cancellationToken);

        if (teacher is null) {
            throw new TeacherNotFoundException();
        }

        return teacher.Id.Value;
    }
}
