//using MediatR;
//using TeachPlanner.Application.Common.Exceptions;
//using TeachPlanner.Application.Common.Interfaces.Persistence;
//using TeachPlanner.Domain.Subjects;

//namespace TeachPlanner.Application.Teachers.Queries.GetSubjectsTaught;
//public class GetSubjectsTaughtQueryHandler : IRequestHandler<GetSubjectsTaughtQuery, GetSubjectsTaughtResult>
//{
//    private readonly ITeacherRepository _teacherRepository;

//    public GetSubjectsTaughtQueryHandler(ITeacherRepository teacherRepository)
//    {
//        _teacherRepository = teacherRepository;
//    }

//    public async Task<GetSubjectsTaughtResult> Handle(GetSubjectsTaughtQuery request, CancellationToken cancellationToken)
//    {
//        List<Subject> subjects = new();

//        if (await _teacherRepository.GetById(request.TeacherId, cancellationToken) == null)
//        {
//            throw new TeacherNotFoundException();
//        }

//        if (request.Elaborations)
//        {
//            subjects = await _teacherRepository.GetSubjectsTaughtByTeacherWithElaborations(request.TeacherId, cancellationToken);
//        }
//        else
//        {
//            subjects = await _teacherRepository.GetSubjectsTaughtByTeacherWithoutElaborations(request.TeacherId, cancellationToken);
//        }

//        return new GetSubjectsTaughtResult(subjects);
//    }
//}
