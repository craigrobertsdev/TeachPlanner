using Application.Common.Interfaces.Curriculum;
using Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace Application.Curriculum.ParseCurriculum;

public class ParseCurriculumCommandHandler : IRequestHandler<ParseCurriculumCommand, ErrorOr<ParseCurriculumResult>>
{
    private readonly ICurriculumParser _curriculumParser;
    private readonly ICurriculumRepository _curriculumRepository;

    public ParseCurriculumCommandHandler(ICurriculumParser curriculumParser, ICurriculumRepository curriculumRepository)
    {
        _curriculumParser = curriculumParser;
        _curriculumRepository = curriculumRepository;
    }
    public async Task<ErrorOr<ParseCurriculumResult>> Handle(ParseCurriculumCommand request, CancellationToken cancellationToken)
    {
        var subjects = _curriculumParser.ParseCurriculum();

        await _curriculumRepository.SaveCurriculum(subjects);

        return new ParseCurriculumResult();
    }
}
