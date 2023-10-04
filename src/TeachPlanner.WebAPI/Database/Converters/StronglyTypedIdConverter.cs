using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.Calendar;
using TeachPlanner.Api.Domain.Common.Planner;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.Reports;
using TeachPlanner.Api.Domain.Resources;
using TeachPlanner.Api.Domain.Students;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.TermPlanners;
using TeachPlanner.Api.Domain.Users;
using TeachPlanner.Api.Domain.WeekPlanners;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Database.Converters;

public static class StronglyTypedIdConverter
{
    public class AssessmentIdConverter : ValueConverter<AssessmentId, Guid>
    {
        public AssessmentIdConverter()
            : base(id => id.Value, value => new AssessmentId(value), null)
        {
        }
    }

    public class CalendarIdConverter : ValueConverter<CalendarId, Guid>
    {
        public CalendarIdConverter()
            : base(id => id.Value, value => new CalendarId(value), null)
        {
        }
    }

    public class LessonPlanIdConverter : ValueConverter<LessonPlanId, Guid>
    {
        public LessonPlanIdConverter()
            : base(id => id.Value, value => new LessonPlanId(value), null)
        {
        }
    }

    public class ReportIdConverter : ValueConverter<ReportId, Guid>
    {
        public ReportIdConverter()
            : base(id => id.Value, value => new ReportId(value), null)
        {
        }
    }

    public class ResourceIdConverter : ValueConverter<ResourceId, Guid>
    {
        public ResourceIdConverter()
            : base(id => id.Value, value => new ResourceId(value), null)
        {
        }
    }

    public class SchoolEventIdConverter : ValueConverter<SchoolEventId, Guid>
    {
        public SchoolEventIdConverter()
            : base(id => id.Value, value => new SchoolEventId(value), null)
        {
        }
    }

    public class StudentIdConverter : ValueConverter<StudentId, Guid>
    {
        public StudentIdConverter()
            : base(id => id.Value, value => new StudentId(value), null)
        {
        }
    }

    public class SubjectIdConverter : ValueConverter<SubjectId, Guid>
    {
        public SubjectIdConverter()
            : base(id => id.Value, value => new SubjectId(value), null)
        {
        }
    }

    public class TeacherIdConverter : ValueConverter<TeacherId, Guid>
    {
        public TeacherIdConverter()
            : base(id => id.Value, value => new TeacherId(value), null)
        {
        }
    }

    public class TermPlannerIdConverter : ValueConverter<TermPlannerId, Guid>
    {
        public TermPlannerIdConverter()
            : base(id => id.Value, value => new TermPlannerId(value), null)
        {
        }
    }

    public class UserIdConverter : ValueConverter<UserId, Guid>
    {
        public UserIdConverter()
            : base(id => id.Value, value => new UserId(value), null)
        {
        }
    }

    public class WeekPlannerIdConverter : ValueConverter<WeekPlannerId, Guid>
    {
        public WeekPlannerIdConverter()
            : base(id => id.Value, value => new WeekPlannerId(value), null)
        {
        }
    }

    public class YearDataIdConverter : ValueConverter<YearDataId, Guid>
    {
        public YearDataIdConverter()
            : base(id => id.Value, value => new YearDataId(value), null)
        {
        }
    }
}
