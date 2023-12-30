using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Contracts.Resources;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Common.Interfaces.Persistence;

namespace TeachPlanner.Blazor.Features.Teachers;

public static class GetResources {
    public static async Task<IResult> Delegate([FromRoute] Guid teacherId, [FromRoute] Guid subjectId, ISender sender,
        CancellationToken cancellationToken) {
        var query = new Query(new TeacherId(teacherId), new SubjectId(subjectId));

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result);
    }

    public record Query(TeacherId TeacherId, SubjectId SubjectId)
        : IRequest<List<ResourceResponse>>;

    public sealed class Handler : IRequestHandler<Query, List<ResourceResponse>> {
        private readonly ITeacherRepository _teacherRepository;

        public Handler(ITeacherRepository teacherRepository) {
            _teacherRepository = teacherRepository;
        }

        public async Task<List<ResourceResponse>> Handle(
            Query request,
            CancellationToken cancellationToken
        ) {
            var resources = await _teacherRepository.GetResourcesBySubject(
                request.TeacherId,
                request.SubjectId,
                cancellationToken
            );

            var response = new List<ResourceResponse>();

            foreach (var resource in resources)
                response.Add(new ResourceResponse(resource.Name, resource.Url, resource.IsAssessment));

            return response;
        }
    }
}