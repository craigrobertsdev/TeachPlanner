using MediatR;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.Resources.DomainEvents;

namespace TeachPlanner.Api.Domain.EventHandlers;

public sealed class ResourceCreatedDomainEventHandler : INotificationHandler<ResourceCreatedDomainEvent>
{
  private readonly ITeacherRepository _teacherRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly ILogger _logger;

  public ResourceCreatedDomainEventHandler(ITeacherRepository teacherRepository, ILogger logger)
  {
    _teacherRepository = teacherRepository;
    _logger = logger;

  }

  public async Task Handle(ResourceCreatedDomainEvent notification, CancellationToken cancellationToken)
  {
    var teacher = _teacherRepository.GetById(notification.TeacherId, cancellationToken);

    if (teacher is null)
    {
      _logger.Log(LogLevel.Error, $"DomainEvent {notification.Id}. Teacher {notification.TeacherId} not found");
      return;
    }

    teacher.AddResource(notification.ResourceId);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}
