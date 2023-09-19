using MediatR;
using TeachPlanner.Application.Common.Interfaces.Curriculum;
using TeachPlanner.Application.Common.Interfaces.Persistence;

namespace TeachPlanner.Application.Curriculum.Commands.ParseCurriculum;

public class ParseCurriculumCommandHandler : IRequestHandler<ParseCurriculumCommand>
{
    private readonly ICurriculumParser _curriculumParser;
    private readonly ICurriculumRepository _curriculumRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ParseCurriculumCommandHandler(ICurriculumParser curriculumParser, ICurriculumRepository curriculumRepository, IUnitOfWork unitOfWork)
    {
        _curriculumParser = curriculumParser;
        _curriculumRepository = curriculumRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(ParseCurriculumCommand request, CancellationToken cancellationToken)
    {
        var subjects = _curriculumParser.ParseCurriculum();

        await _curriculumRepository.SaveCurriculum(subjects, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
