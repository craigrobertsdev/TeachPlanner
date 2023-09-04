using ErrorOr;
using MediatR;

namespace TeachPlanner.Application.Curriculum.Commands.ParseCurriculum;

public record ParseCurriculumCommand() : IRequest<ErrorOr<ParseCurriculumResult>>;
