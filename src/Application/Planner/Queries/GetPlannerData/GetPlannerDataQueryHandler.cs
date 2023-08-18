using Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace Application.Planner.Queries.GetPlannerData;

public class GetPlannerDataQueryHandler //: IRequestHandler<GetPlannerDataQuery, ErrorOr<GetPlannerDataResult>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ISubjectRepository _subjectRepository;

    public GetPlannerDataQueryHandler(
        ITeacherRepository teacherRepository,
        ISubjectRepository subjectRepository)
    {
        _teacherRepository = teacherRepository;
        _subjectRepository = subjectRepository;
    }

    // public async Task<ErrorOr<GetPlannerDataResult>> Handle(GetPlannerDataQuery request, CancellationToken cancellationToken)
    // {
    //     var teacher = await _teacherRepository.GetTeacherById(request.TeacherId);

    //     if (teacher is null)
    //     {
    //         return new NotFoundError($"Teacher with id {request.TeacherId} not found");
    //     }

    //     var weekPlanner = await _weekPlannerRepository.GetWeekPlanner(teacher.WeekPlannerId);

    //     if (weekPlanner is null)
    //     {
    //         return new NotFoundError($"WeekPlanner with id {teacher.WeekPlannerId} not found");
    //     }

    //     var dayPlanPattern = await _dayPlanPatternRepository.GetDayPlanPattern(weekPlanner.DayPlanPatternId);

    //     if (dayPlanPattern is null)
    //     {
    //         return new NotFoundError($"DayPlanPattern with id {weekPlanner.DayPlanPatternId} not found");
    //     }

    //     var subjects = await _subjectRepository.GetSubjects(teacher.SubjectIds);

    //     return new GetPlannerDataResult(weekPlanner, dayPlanPattern, subjects);
    // // }
}