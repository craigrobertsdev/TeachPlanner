using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Database.QueryExtensions;

public static class SubjectQueries
{
    public static async Task<List<Subject>> GetSubjectsTaughtByTeacherWithElaborations(
        this ApplicationDbContext context,
        TeacherId teacherId,
        CancellationToken cancellationToken)
    {
        var subjectIds = context.Teachers
            .Where(t => t.Id == teacherId)
            .SelectMany(t => t.SubjectsTaughtIds)
            .ToList();

        var subjectQuery = context.Subjects
            .AsNoTracking()
            .Where(s => subjectIds.Contains(s.Id))
            .Where(s => s.Name != "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.Substrands!)
            .ThenInclude(s => s.ContentDescriptions)
            .ThenInclude(cd => cd.Elaborations);

        var mathsQuery = context.Subjects
            .AsNoTracking()
            .Where(s => subjectIds.Contains(s.Id))
            .Where(s => s.Name == "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions!)
            .ThenInclude(cd => cd.Elaborations);


        var subjects = await subjectQuery.ToListAsync(cancellationToken);
        var maths = await mathsQuery.SingleOrDefaultAsync(cancellationToken);

        if (maths != null)
        {
            subjects.Add(maths);
        }

        if (subjects.Count == 0)
        {
            return new List<Subject>();
        }

        return subjects;
    }

    public static async Task<List<Subject>> GetTermPlannerSubjectsWithoutElaborations(
        this ApplicationDbContext context,
        List<SubjectId> subjectIds,
        CancellationToken cancellationToken)
    {
        var subjectQuery = context.Subjects
            .AsNoTracking()
            .Where(s => subjectIds.Contains(s.Id))
            .Where(s => s.Name != "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.Substrands!)
            .ThenInclude(s => s.ContentDescriptions);

        var mathsQuery = context.Subjects
            .AsNoTracking()
            .Where(s => subjectIds.Contains(s.Id))
            .Where(s => s.Name == "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions!);

        var subjects = await subjectQuery.ToListAsync(cancellationToken);
        var maths = await mathsQuery.SingleOrDefaultAsync(cancellationToken);

        if (maths != null)
        {
            subjects.Add(maths);
        }

        if (subjects.Count == 0)
        {
            return new List<Subject>();
        }

        return subjects;
    }
    public static async Task<List<Subject>> GetCurriculum(this ApplicationDbContext context, CancellationToken cancellationToken)
    {
        var subjectQuery = context.Subjects
            .AsNoTracking()
            .Where(s => s.Name != "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.Substrands!)
            .ThenInclude(s => s.ContentDescriptions)
            .ThenInclude(cd => cd.Elaborations);

        var mathsQuery = context.Subjects
            .AsNoTracking()
            .Where(s => s.Name == "Mathematics")
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions!)
            .ThenInclude(cd => cd.Elaborations);

        var subjects = await subjectQuery.ToListAsync(cancellationToken);
        var maths = await mathsQuery.SingleOrDefaultAsync(cancellationToken);

        if (maths != null)
        {
            subjects.Add(maths);
        }

        if (subjects.Count == 0)
        {
            return new List<Subject>();
        }

        return subjects;
    }
}
