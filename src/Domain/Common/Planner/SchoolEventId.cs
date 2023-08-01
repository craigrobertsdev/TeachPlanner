using Microsoft.EntityFrameworkCore;

namespace Domain.Common.Planner;

[Keyless]
public record SchoolEventId(Guid Value);
