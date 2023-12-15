using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Curriculum;
using TeachPlanner.Api.Contracts.LessonPlans;
using TeachPlanner.Api.Contracts.Resources;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Features.LessonPlans;
/// <summary>
/// This endpoint is called by the client when a teacher creates a blank lesson plan in their planner, and provides the necessary data for them to be able to plan any subject that they teach.
/// </summary>
public static class GetDataForBlankLessonPlan {
    public record Query(TeacherId TeacherId, int CalendarYear) : IRequest<GetLessonPlanDataResponse>;

    public sealed class Handler : IRequestHandler<Query, GetLessonPlanDataResponse> {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ICurriculumRepository _curriculumRepository;
        private readonly IYearDataRepository _yearDataRepository;


        public Handler(ITeacherRepository teacherRepository, ICurriculumRepository curriculumRepository, IYearDataRepository yearDataRepository) {
            _teacherRepository = teacherRepository;
            _curriculumRepository = curriculumRepository;
            _yearDataRepository = yearDataRepository;

        }
        public async Task<GetLessonPlanDataResponse> Handle(Query request, CancellationToken cancellationToken) {
            var teacher = await _teacherRepository.GetWithResources(request.TeacherId, cancellationToken);

            if (teacher == null) {
                throw new TeacherNotFoundException();
            }

            var yearLevels = await _yearDataRepository.GetYearLevelsTaught(request.TeacherId, request.CalendarYear, cancellationToken);
            var subjects = await _curriculumRepository.GetSubjectsByYearLevels(yearLevels.ToList(), cancellationToken);

            var contentDescriptionsPerSubject = new Dictionary<string, List<ContentDescriptionDto>>();

            foreach (var subject in subjects) {
                contentDescriptionsPerSubject.Add(subject.Name, new List<ContentDescriptionDto>());
                foreach (var yearLevel in subject.YearLevels) {
                    foreach (var strand in yearLevel.Strands) {
                        foreach (var contentDescription in strand.ContentDescriptions) {
                            contentDescriptionsPerSubject[subject.Name].Add(new ContentDescriptionDto(
                                strand.Name,
                                contentDescription.CurriculumCode,
                                contentDescription.Description));
                        }
                    }
                }
            }

            var subjectDtos = subjects.Select(s => new CurriculumSubjectDto(
                s.Name,
                s.YearLevels.Select(yl => new YearLevelDto(
                    yl.YearLevelValue != null ? yl.YearLevelValue.ToString() : yl.BandLevelValue.ToString(),
                    contentDescriptionsPerSubject[s.Name]
                    .ToList()))
                .ToList()))
            .ToList();

            var resources = teacher.Resources
                .Select(r => new ResourceResponse(r.Name, r.Url, r.IsAssessment)).ToList();

            return new GetLessonPlanDataResponse(subjectDtos, resources);
        }
    }

    public static async Task<IResult> Delegate([FromRoute] Guid teacherId, int calendarYear, ISender sender, CancellationToken cancellationToken) {
        var query = new Query(new TeacherId(teacherId), calendarYear);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result);
    }
}
