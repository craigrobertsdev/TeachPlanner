using MediatR;
using TeachPlanner.Application.Common.Interfaces.Curriculum;
using TeachPlanner.Application.Common.Interfaces.Persistence;

namespace TeachPlanner.Application.Curriculum.Commands.ParseCurriculum;

public class ParseCurriculumCommandHandler : IRequestHandler<ParseCurriculumCommand, ParseCurriculumResult>
{
    private readonly ICurriculumParser _curriculumParser;
    private readonly ICurriculumRepository _curriculumRepository;

    public ParseCurriculumCommandHandler(ICurriculumParser curriculumParser, ICurriculumRepository curriculumRepository)
    {
        _curriculumParser = curriculumParser;
        _curriculumRepository = curriculumRepository;
    }
    public async Task<ParseCurriculumResult> Handle(ParseCurriculumCommand request, CancellationToken cancellationToken)
    {
        var subjects = _curriculumParser.ParseCurriculum();

        await _curriculumRepository.SaveCurriculum(subjects, cancellationToken);

        return new ParseCurriculumResult();
    }
}
