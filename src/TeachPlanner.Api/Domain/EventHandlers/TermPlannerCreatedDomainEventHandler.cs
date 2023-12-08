using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Domain.TermPlanners.DomainEvents;

namespace TeachPlanner.Api.Domain.EventHandlers;

internal sealed class TermPlannerCreatedDomainEventHandler : INotificationHandler<TermPlannerCreatedDomainEvent> {
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public TermPlannerCreatedDomainEventHandler(ApplicationDbContext context, IUnitOfWork unitOfWork) {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(TermPlannerCreatedDomainEvent notification, CancellationToken cancellationToken) {
        var yearData = await _context.YearData
            .Where(yd => yd.Id == notification.YearDataId)
            .FirstAsync(cancellationToken);

        yearData.AddTermPlanner(notification.TermPlannerId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}