using MediatR;
using TeachPlanner.Api.Common.Interfaces.Curriculum;
using TeachPlanner.Api.Common.Interfaces.Persistence;

namespace TeachPlanner.Api.Features.Curriculum;

public static class ParseCurriculum
{
    public record Command() : IRequest;

    internal sealed class Handler : IRequestHandler<Command>
    {
        private readonly ICurriculumParser _curriculumParser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurriculumRepository _curriculumRepository;

        public Handler(ICurriculumParser curriculumParser, IUnitOfWork unitOfWork, ICurriculumRepository curriculumRepository)
        {
            _curriculumParser = curriculumParser;
            _unitOfWork = unitOfWork;
            _curriculumRepository = curriculumRepository;
        }
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var subjects = _curriculumParser.ParseCurriculum();

            await _curriculumRepository.AddCurriculum(subjects, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    public static async Task<IResult> Delegate(ISender sender, CancellationToken cancellationToken)
    {
        var command = new Command();
        await sender.Send(command, cancellationToken);

        return Results.Ok();
    }
}

