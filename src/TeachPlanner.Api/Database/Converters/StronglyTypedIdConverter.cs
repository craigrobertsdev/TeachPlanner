using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.Calendar;
using TeachPlanner.Api.Domain.Common.Planner;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Reports;
using TeachPlanner.Api.Domain.Students;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.TermPlanners;
using TeachPlanner.Api.Domain.Users;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Database.Converters;

public static class StronglyTypedIdConverter
{
    public class AssessmentIdConverter : ValueConverter<AssessmentId, Guid>
    {
        public AssessmentIdConverter()
            : base(id => id.Value, value => new AssessmentId(value))
        {
        }
    }

    public class CalendarIdConverter : ValueConverter<CalendarId, Guid>
    {
        public CalendarIdConverter()
            : base(id => id.Value, value => new CalendarId(value))
        {
        }
    }

    public class LessonPlanIdConverter : ValueConverter<LessonPlanId, Guid>
    {
        public LessonPlanIdConverter()
            : base(id => id.Value, value => new LessonPlanId(value))
        {
        }
    }

    public class ReportIdConverter : ValueConverter<ReportId, Guid>
    {
        public ReportIdConverter()
            : base(id => id.Value, value => new ReportId(value))
        {
        }
    }

    public class ResourceIdConverter : ValueConverter<ResourceId, Guid>
    {
        public ResourceIdConverter()
            : base(id => id.Value, value => new ResourceId(value))
        {
        }
    }

    public class SchoolEventIdConverter : ValueConverter<SchoolEventId, Guid>
    {
        public SchoolEventIdConverter()
            : base(id => id.Value, value => new SchoolEventId(value))
        {
        }
    }

    public class StudentIdConverter : ValueConverter<StudentId, Guid>
    {
        public StudentIdConverter()
            : base(id => id.Value, value => new StudentId(value))
        {
        }
    }

    public class CurriculumSubjectIdConverter : ValueConverter<SubjectId, Guid>
    {
        public CurriculumSubjectIdConverter()
            : base(id => id.Value, value => new SubjectId(value))
        {
        }
    }

    public class TeacherIdConverter : ValueConverter<TeacherId, Guid>
    {
        public TeacherIdConverter()
            : base(id => id.Value, value => new TeacherId(value))
        {
        }
    }

    public class TermPlannerIdConverter : ValueConverter<TermPlannerId, Guid>
    {
        public TermPlannerIdConverter()
            : base(id => id.Value, value => new TermPlannerId(value))
        {
        }
    }

    public class UserIdConverter : ValueConverter<UserId, Guid>
    {
        public UserIdConverter()
            : base(id => id.Value, value => new UserId(value))
        {
        }
    }

    public class WeekPlannerIdConverter : ValueConverter<WeekPlannerId, Guid>
    {
        public WeekPlannerIdConverter()
            : base(id => id.Value, value => new WeekPlannerId(value))
        {
        }
    }

    public class YearDataIdConverter : ValueConverter<YearDataId, Guid>
    {
        public YearDataIdConverter()
            : base(id => id.Value, value => new YearDataId(value))
        {
        }
    }
}