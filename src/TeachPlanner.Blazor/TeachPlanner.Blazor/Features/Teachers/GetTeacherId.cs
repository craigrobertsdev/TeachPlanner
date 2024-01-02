using TeachPlanner.Shared.Common.Interfaces.Services;

namespace TeachPlanner.Blazor.Features.Teachers;

public static class GetTeacherId {
    public static async Task<IResult> Delegate(ITeacherService teacherService, string userId, CancellationToken cancellationToken) {
        var result = await teacherService.GetTeacherId(userId, cancellationToken);

        return Results.Ok(result);
    }
}
