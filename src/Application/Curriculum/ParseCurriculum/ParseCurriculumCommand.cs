using ErrorOr;
using MediatR;

namespace Application.Curriculum.ParseCurriculum;

public record ParseCurriculumCommand() : IRequest<ErrorOr<ParseCurriculumResult>>;
