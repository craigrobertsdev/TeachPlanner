using MediatR;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;

namespace TeachPlanner.Application.Teachers.Commands.SetSubjectsTaught;
public class SetSubjectsTaughtCommandHandler : IRequestHandler<SetSubjectsTaughtCommand>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetSubjectsTaughtCommandHandler(ITeacherRepository teacherRepository, ISubjectRepository subjectRepository, IUnitOfWork unitOfWork)
    {
        _teacherRepository = teacherRepository;
        _subjectRepository = subjectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SetSubjectsTaughtCommand command, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetById(command.TeacherId, cancellationToken);

        if (teacher == null)
        {
            throw new TeacherNotFoundException();
        }

        // do something here to create a list of subjects from the subjectIds
        // should subjectIds actually be subjects and we just ignore the Ids? they aren't useful to us here
        // the user will send a payload of subject shaped objects to the api
        // the user will already have been provided with a list of subjects taught by the teacher
        // the provided subjects will have the correct ids, however the new subjects won't
        // maybe we do need to go back to the subjectName list
        // we will query the curriculumRepository for the subjects with the names in the list
        // create new subjects with those details and save the new subjects to the teacher'sId list of taught subjects
        // data needed will include the year levels, or will this be done via a separate endpoint?

        // this needs to handle 2 cases
        // where the teacher has no subjects taught that are in the list of subjectIds


        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
