using MediatR;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Application.Teachers.Queries.GetTeacherSettings;
public class GetTeacherSettingsQueryHandler : IRequestHandler<GetTeacherSettingsQuery, GetTeacherSettingsResult>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ICurriculumRepository _curriculumRepository;
    private readonly IYearDataRepository _yearDataRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetTeacherSettingsQueryHandler(ITeacherRepository teacherRepository,
        ICurriculumRepository curriculumRepository, IYearDataRepository yearDataRepository, IUnitOfWork unitOfWork)
    {
        _teacherRepository = teacherRepository;
        _curriculumRepository = curriculumRepository;
        _yearDataRepository = yearDataRepository;
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
        var yearData = await _yearDataRepository.GetByTeacherAndYear(teacher.Id, request.CalendarYear, cancellationToken);

        return new GetTeacherSettingsResult(
            curriculumSubjects,
            yearData ?? YearData.Create(request.CalendarYear));
    }
}
