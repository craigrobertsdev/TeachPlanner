using TeachPlanner.Api.UnitTests.Helpers.Interfaces;

namespace TeachPlanner.Api.UnitTests.Helpers.Classes;
internal class FakeServiceProvider : IFakeServiceProvider {
    public void DoNothing() {
    }

    public object? GetService(Type serviceType) {
        return new object();
    }
}
