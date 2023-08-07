using ErrorOr;
using MediatR;

namespace Application.Curriculum.Commands.ParseCurriculum;

public record ParseCurriculumCommand() : IRequest<ErrorOr<ParseCurriculumResult>>;
