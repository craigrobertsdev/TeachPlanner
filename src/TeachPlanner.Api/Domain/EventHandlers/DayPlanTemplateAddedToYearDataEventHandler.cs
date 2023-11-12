using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Domain.YearDataRecords.DomainEvents;

namespace TeachPlanner.Api.Domain.EventHandlers;

public class DayPlanTemplateAddedToYearDataEventHandler : INotificationHandler<DayPlanTemplateAddedToYearDataEvent>
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DayPlanTemplateAddedToYearDataEventHandler(ApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DayPlanTemplateAddedToYearDataEvent notification, CancellationToken cancellationToken)
    {
        var yearData = await _context.YearData
            .Where(yd => yd.DayPlanTemplate != null && yd.DayPlanTemplate.Id == notification.DayPlanTemplate.Id)
            .Include(yd => yd.WeekPlanners)
            .FirstAsync(cancellationToken);

        foreach (var weekPlanner in yearData.WeekPlanners)
        {
            weekPlanner.SetDayPlanTemplate(notification.DayPlanTemplate);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

}
