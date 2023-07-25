using Domain.SubjectAggregates.ValueObjects;

namespace Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class Subject
    {
        public static SubjectId Id = new(Guid.NewGuid());
    }
}
