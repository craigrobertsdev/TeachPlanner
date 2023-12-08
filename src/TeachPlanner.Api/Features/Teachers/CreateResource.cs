using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Resources;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Features.Teachers;

public static class CreateResource {
    public static async Task<IResult> Delegate(ISender sender, Guid teacherId, CreateResourceRequest request,
        CancellationToken cancellationToken) {
        var command = new Command(new TeacherId(teacherId), request.Name, new SubjectId(request.SubjectId),
            request.AssociatedStrands, request.IsAssessment);

        var result = await sender.Send(command, cancellationToken);

        return Results.Ok(result);
    }

    public record Command(TeacherId TeacherId, string Name, SubjectId SubjectId, List<string> AssociatedStrands,
        bool IsAssessment) : IRequest<string>;

    public sealed class Handler : IRequestHandler<Command, string> {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork) {
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(Command request, CancellationToken cancellationToken) {
            // TODO: Add method to upload files to storage and generate URL
            var url = "https://www.placeholder.com";

            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

            if (teacher is null) throw new TeacherNotFoundException();

            var resource = Resource.Create(
                request.TeacherId,
                request.Name,
                url,
                request.IsAssessment,
                request.SubjectId,
                request.AssociatedStrands);

            teacher.AddResource(resource);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return url;
        }
    }
}