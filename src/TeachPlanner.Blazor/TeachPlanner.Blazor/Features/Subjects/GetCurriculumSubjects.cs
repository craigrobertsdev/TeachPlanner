using MediatR;
using TeachPlanner.Blazor.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Blazor.Features.Subjects;

public static class GetCurriculumSubjects {
    public static async Task<IResult> Delegate(bool includeElaborations, ISender sender,
        CancellationToken cancellationToken) {
        var query = new Query(includeElaborations);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result);
    }

    public record Query(bool IncludeElaborations) : IRequest<List<CurriculumSubject>>;

    public sealed class Handler : IRequestHandler<Query, List<CurriculumSubject>> {
        private readonly ISubjectRepository _subjectRepository;

        public Handler(ISubjectRepository subjectRepository) {
            _subjectRepository = subjectRepository;
        }

        public async Task<List<CurriculumSubject>> Handle(Query request, CancellationToken cancellationToken) {
            var subjects =
                await _subjectRepository.GetCurriculumSubjects(request.IncludeElaborations, cancellationToken);

            return subjects;
        }
    }
}