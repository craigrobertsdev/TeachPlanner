using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Database;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.YearDataRecords.DomainEvents;

namespace TeachPlanner.Shared.Domain.EventHandlers;

internal sealed class YearDataCreatedDomainEventHandler : INotificationHandler<YearDataCreatedDomainEvent> {
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public YearDataCreatedDomainEventHandler(ApplicationDbContext context, IUnitOfWork unitOfWork) {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(YearDataCreatedDomainEvent notification, CancellationToken cancellationToken) {
        var teacher = await _context.Teachers
            //.Where(t => t.Id == notification.TeacherId)
            .FirstAsync(cancellationToken);

        teacher.AddYearData(YearDataEntry.Create(notification.CalendarYear, notification.YearDataId));

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}