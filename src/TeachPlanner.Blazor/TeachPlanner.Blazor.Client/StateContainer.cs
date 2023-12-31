using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Blazor.Client;

public class StateContainer {
    private TeacherId _teacherId = null!;
    public TeacherId TeacherId {
        get => _teacherId;
        set {
            _teacherId = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
