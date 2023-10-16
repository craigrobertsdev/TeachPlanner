using MediatR;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Resources;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Features.Resources;

public static class GetResources
{
    public record Query(TeacherId TeacherId, SubjectId SubjectId)
        : IRequest<List<ResourceResponse>>;

    public sealed class Handler : IRequestHandler<Query, List<ResourceResponse>>
    {
        private readonly IResourceRepository _resourceRepository;

        public Handler(IResourceRepository resourceRepository, ITeacherRepository teacherRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<List<ResourceResponse>> Handle(
            Query request,
            CancellationToken cancellationToken
        )
        {
            var resources = await _resourceRepository.GetByTeacherAndSubject(
                request.TeacherId,
                request.SubjectId,
                cancellationToken
            );

            var response = new List<ResourceResponse>();

            foreach (var resource in resources)
            {
                response.Add(
                    new ResourceResponse(resource.Name, resource.Url, resource.IsAssessment)
                );
            }

            return response;
        }
    }
}
