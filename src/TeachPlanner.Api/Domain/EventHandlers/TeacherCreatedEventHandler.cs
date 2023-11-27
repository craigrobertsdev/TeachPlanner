using MediatR;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers.DomainEvents;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Domain.EventHandlers;

public class TeacherCreatedDomainEventHandler : INotificationHandler<TeacherCreatedDomainEvent>
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public TeacherCreatedDomainEventHandler(ApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(TeacherCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var dayPlanTemplate = DayPlanTemplate.Create(new(), notification.TeacherId);
        var yearData = YearData.Create(notification.TeacherId, DateTime.Now.Year, dayPlanTemplate);

        _context.YearData.Add(yearData);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}