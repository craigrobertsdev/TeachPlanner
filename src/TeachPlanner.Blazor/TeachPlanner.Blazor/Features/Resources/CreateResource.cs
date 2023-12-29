using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Contracts.Resources;
using TeachPlanner.Blazor.Common.Interfaces.Persistence;
using TeachPlanner.Blazor.Common.Interfaces.Services;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Blazor.Features.Resources;

public static class CreateResource {
    public record Command(TeacherId TeacherId, Stream File, string Name, SubjectId SubjectId, bool isAssessment) : IRequest;

    public sealed class Handler : IRequestHandler<Command> {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStorageManager _storageManager;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITeacherRepository teacherRepsoitory, IStorageManager storageManager, IUnitOfWork unitOfWork) {
            _teacherRepository = teacherRepsoitory;
            _storageManager = storageManager;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(Command request, CancellationToken cancellationToken) {
            var teacher = await _teacherRepository.GetWithResources(request.TeacherId, cancellationToken);

            if (teacher is null) {
                throw new TeacherNotFoundException();
            }

            var url = await _storageManager.UploadResource(request.File, cancellationToken);

            var resource = Resource.Create(request.TeacherId, request.Name, url, request.isAssessment, request.SubjectId, null);
            teacher.AddResource(resource);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    //public static async Task<IResult> Delegate(Guid teacherId, CreateResourceRequest request, ISender sender, CancellationToken cancellationToken) {
    //    var command = new Command(new TeacherId(teacherId), request.FileStream, request.Name, request.SubjectId, request.IsAssessment);
    //}
}
