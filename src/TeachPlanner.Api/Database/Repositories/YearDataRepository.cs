﻿using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Database.Repositories;

public class YearDataRepository : IYearDataRepository
{
    private readonly ApplicationDbContext _context;

    public YearDataRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<YearData?> GetByTeacherIdAndYear(TeacherId teacherId, int calendarYear,
        CancellationToken cancellationToken)
    {
        return await _context.YearData
            .Where(yd => yd.TeacherId == teacherId)
            .Where(yd => yd.CalendarYear == calendarYear)
            .Include(yd => yd.Subjects)
            .Include(yd => yd.Students)
            .Include(yd => yd.WeekPlanners)
            .Include(yd => yd.LessonPlans)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task SetInitialAccountDetails(Teacher teacher, List<string> yearLevelsTaught, DayPlanTemplate dayPlanTemplate,
               int calendarYear, CancellationToken cancellationToken)
    {
        var yearData = await _context.YearData
            .Where(yd => yd.TeacherId == teacher.Id)
            .Where(yd => yd.CalendarYear == calendarYear)
            .FirstOrDefaultAsync(cancellationToken);

        if (yearData is null)
        {
            yearData = YearData.Create(teacher.Id, calendarYear, yearLevelsTaught);
            teacher.AddYearData(YearDataEntry.Create(calendarYear, yearData.Id));
            _context.YearData.Add(yearData);
        }

        yearData.SetDayPlanTemplate(dayPlanTemplate);
    }
}