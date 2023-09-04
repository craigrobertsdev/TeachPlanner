using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.LessonPlans;

public sealed class LessonComment : ValueObject
{
    public string Content { get; private set; }
    public bool Completed { get; private set; }
    public bool StruckOut { get; private set; }
    public DateTime? CompletedDateTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public void Update(string content)
    {
        Content = content;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public void Complete()
    {
        Completed = true;
        CompletedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public void RemoveCompletion()
    {
        Completed = false;
        CompletedDateTime = null;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public void StrikeOut()
    {
        StruckOut = true;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public void RemoveStrikeOut()
    {
        StruckOut = false;
        UpdatedDateTime = DateTime.UtcNow;
    }

    private LessonComment(
        string content,
        bool completed,
        bool struckThrough,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        DateTime? completedDateTime)
    {
        Content = content;
        Completed = completed;
        StruckOut = struckThrough;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        CompletedDateTime = completedDateTime;
    }

    public static LessonComment Create(string content)
    {
        return new LessonComment(content, false, false, DateTime.UtcNow, DateTime.UtcNow, null);
    }
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Content;
        yield return Completed;
        yield return StruckOut;
        yield return CompletedDateTime;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LessonComment() { }
}
