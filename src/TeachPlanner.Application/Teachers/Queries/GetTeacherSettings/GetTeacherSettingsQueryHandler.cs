using MediatR;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Application.Teachers.Queries.GetTeacherSettings;
public class GetTeacherSettingsQueryHandler : IRequestHandler<GetTeacherSettingsQuery, GetTeacherSettingsResult>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ICurriculumRepository _curriculumRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetTeacherSettingsQueryHandler(ITeacherRepository teacherRepository, ICurriculumRepository curriculumRepository, IUnitOfWork unitOfWork)
    {
        _teacherRepository = teacherRepository;
        _curriculumRepository = curriculumRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetTeacherSettingsResult> Handle(GetTeacherSettingsQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

        if (teacher == null)
        {
            throw new TeacherNotFoundException();
        }

        var curriculumSubjects = await _curriculumRepository.GetCurriculumSubjectNamesAndIds(cancellationToken);
        var yearData = teacher.GetYearData(request.CalendarYear);

        return new GetTeacherSettingsResult(
            curriculumSubjects,
            yearData ?? YearData.Create(request.CalendarYear));
    }
}
