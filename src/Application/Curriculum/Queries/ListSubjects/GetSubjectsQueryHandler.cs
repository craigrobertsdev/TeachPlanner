using Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace Application.Curriculum.Queries.ListSubjects;
public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, ErrorOr<GetSubjectsResult>>
{
    private readonly ICurriculumRepository _curriculumRepository;

    public GetSubjectsQueryHandler(ICurriculumRepository curriculumRepository)
    {
        _curriculumRepository = curriculumRepository;
    }
    public async Task<ErrorOr<GetSubjectsResult>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
    {
        var subjects = request.Elaborations == true ? await _curriculumRepository.GetSubjects() : await _curriculumRepository.GetSubjectsWithoutElaborations();

        return new GetSubjectsResult(subjects);
    }
}
