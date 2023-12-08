using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Curriculum;
using TeachPlanner.Api.Contracts.LessonPlans;
using TeachPlanner.Api.Contracts.Resources;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Features.LessonPlans;

public static class GetLessonPlanData {
    public record Query(TeacherId TeacherId, List<YearLevelValue> YearLevels) : IRequest<GetLessonPlanDataResponse>;

    public sealed class Handler : IRequestHandler<Query, GetLessonPlanDataResponse> {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ICurriculumRepository _curriculumRepository;

        public Handler(ITeacherRepository teacherRepository, ICurriculumRepository curriculumRepository) {
            _teacherRepository = teacherRepository;
            _curriculumRepository = curriculumRepository;
        }
        public async Task<GetLessonPlanDataResponse> Handle(Query request, CancellationToken cancellationToken) {
            var teacher = await _teacherRepository.GetWithResources(request.TeacherId, cancellationToken);

            if (teacher == null) {
                throw new TeacherNotFoundException();
            }

            // implement ability to get subjects based on their year level
            var subjects = await _curriculumRepository.GetSubjectsByYearLevels(request.YearLevels, cancellationToken);

            var subjectDtos = subjects.Select(s => new CurriculumSubjectDto(
                s.Name,
                s.YearLevels.Select(yl => new YearLevelDto(
                    yl.YearLevelValue != null ? yl.YearLevelValue.ToString() : yl.BandLevelValue.ToString(),
                    yl.GetContentDescriptions().Select(cd => new ContentDescriptionDto(cd.CurriculumCode, cd.Description))
                    .ToList()))
                .ToList()))
            .ToList();

            var resources = teacher.Resources
                .Select(r => new ResourceResponse(r.Name, r.Url, r.IsAssessment)).ToList();

            return new GetLessonPlanDataResponse(subjectDtos, resources);
        }
    }

    public static async Task<IResult> Delegate([FromRoute] Guid teacherId, [AsParameters] List<string> yearLevels, ISender sender, CancellationToken cancellationToken) {
        var query = new Query(new TeacherId(teacherId), yearLevels.Select(Enum.Parse<YearLevelValue>).ToList());

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result);
    }
}
