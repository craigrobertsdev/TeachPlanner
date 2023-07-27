using Domain.ResourceAggregate.ValueObjects;

namespace Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class Resource
    {
        public static ResourceId Id = new(Guid.NewGuid());
        public const string Name = "Resource Name";
        public const string Description = "Resource Description";
        public const string Url = "https://www.google.com";
    }
}
