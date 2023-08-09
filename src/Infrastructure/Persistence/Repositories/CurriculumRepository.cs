﻿using Application.Common.Interfaces.Persistence;
using Domain.SubjectAggregates;
using Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public class CurriculumRepository : ICurriculumRepository
{
    private readonly ApplicationDbContext _context;

    public CurriculumRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Subject>> GetAllSubjects()
    {
        var subjects = _context.Subjects
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.Substrands!)
            .ThenInclude(s => s.ContentDescriptions)
            .ThenInclude(cd => cd.Elaborations)
            .Where(s => s.Name != "Mathematics")
            .ToList();

        var maths = _context.Subjects
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions!)
            .ThenInclude(cd => cd.Elaborations)
            .Where(s => s.Name == "Mathematics")
            .ToList();

        subjects.AddRange(maths);

        return Task.FromResult(subjects);

    }

    public Task SaveCurriculum(List<Subject> subjects)
    {
        foreach (var subject in subjects)
        {
            _context.Subjects.Add(subject);
        }

        return _context.SaveChangesAsync();
    }


}