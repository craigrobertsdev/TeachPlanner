﻿using TeachPlanner.Api.Entities.Teachers;
using TeachPlanner.Api.Entities.YearDataRecords;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;
public interface IYearDataRepository : IRepository<YearData>
{
    Task<YearData?> GetByTeacherAndYear(TeacherId teacherId, int calendarYear, CancellationToken cancellationToken);
}
