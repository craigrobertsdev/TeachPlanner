using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using TeachPlanner.Shared.Contracts.Curriculum;
using TeachPlanner.Shared.Contracts.LessonPlans;

namespace TeachPlanner.Blazor.Client.Pages.Planner;

public partial class LessonPlan {
    [Inject]
    ApplicationState AppState { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    public HttpClient Http { get; set; } = null!;
    // public ILessonPlanService LessonPlanService { get; set; }
    private string SelectedSubject { get; set; } = string.Empty;
    private List<CurriculumSubjectDto> Subjects { get; set; } = new();

    protected override async Task OnInitializedAsync() {
        if (NavigationManager.Uri.Contains("create")) {
            var response = await Http.GetFromJsonAsync<GetLessonPlanDataResponse>($"/api/{AppState.TeacherId?.Value.ToString()}/lesson-plans/data");
        }
        Subjects = new() {
            new CurriculumSubjectDto("Mathematics", new List<YearLevelDto>()),
            new CurriculumSubjectDto("Science", new List<YearLevelDto>()),
            new CurriculumSubjectDto("English", new List<YearLevelDto>()),
            new CurriculumSubjectDto("Humanities and Social Sciences", new List<YearLevelDto>()),
            new CurriculumSubjectDto("Health and Physical Education", new List<YearLevelDto>()),
        };

        SelectedSubject = Subjects[0].Name;

        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
    }

    private void OnSubjectSelectionChange(ChangeEventArgs e) {
        if (e.Value is not null) {
            SelectedSubject = e.Value.ToString();
        }
    }
}
