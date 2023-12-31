﻿using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.Teachers.DomainEvents;
using TeachPlanner.Api.Domain.Users;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Domain.Teachers;

public sealed class Teacher : Entity<TeacherId>, IAggregateRoot {
    private readonly List<Assessment> _assessments = new();
    private readonly List<Resource> _resources = new();
    private readonly List<YearDataEntry> _yearDataHistory = new();
    private readonly List<CurriculumSubject> _subjectsTaught = new();

    private Teacher(TeacherId id, UserId userId, string firstName, string lastName) : base(id) {
        FirstName = firstName;
        LastName = lastName;
        UserId = userId;
    }

    public UserId UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public IReadOnlyList<Assessment> Assessments => _assessments.AsReadOnly();
    public IReadOnlyList<Resource> Resources => _resources.AsReadOnly();
    public IReadOnlyList<YearDataEntry> YearDataHistory => _yearDataHistory.AsReadOnly();
    public IReadOnlyList<CurriculumSubject> SubjectsTaught => _subjectsTaught.AsReadOnly();

    public static Teacher Create(UserId userId, string firstName, string lastName) {
        var teacher = new Teacher(new TeacherId(Guid.NewGuid()), userId, firstName, lastName);
        teacher.AddDomainEvent(new TeacherCreatedDomainEvent(Guid.NewGuid(), teacher.Id));

        return teacher;
    }

    public YearDataId? GetYearData(int year) {
        var yearDataEntry = _yearDataHistory.FirstOrDefault(yd => yd.CalendarYear == year);

        if (yearDataEntry is null) return null;

        return yearDataEntry.YearDataId;
    }

    public void AddYearData(YearDataEntry yearDataEntry) {
        if (!YearDataExists(yearDataEntry))
            _yearDataHistory.Add(YearDataEntry.Create(yearDataEntry.CalendarYear, yearDataEntry.YearDataId));
    }

    private bool YearDataExists(YearDataEntry yearDataEntry) {
        return YearDataExists(yearDataEntry.CalendarYear);
    }

    private bool YearDataExists(int year) {
        return _yearDataHistory.FirstOrDefault(yd => yd.CalendarYear == year) is not null;
    }

    public void AddResource(Resource resource) {
        _resources.Add(resource);
    }

    public void SetSubjectsTaught(List<CurriculumSubject> subjects) {
        foreach (var subject in _subjectsTaught.ToList()) {
            if (!subjects.Contains(subject)) {
                _subjectsTaught.Remove(subject);
            }
        }

        foreach (var subject in subjects) {
            if (_subjectsTaught.Contains(subject)) {
                continue;
            }

            _subjectsTaught.Add(subject);
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Teacher() {
    }
}