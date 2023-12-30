using TeachPlanner.Blazor.Client.Common.Interfaces;

namespace TeachPlanner.Blazor.Client.Services;

public class LessonPlanService : ILessonPlanService{
    private readonly HttpClient _httpClient;

    public LessonPlanService(HttpClient httpClient) {
        _httpClient = httpClient;
    }
}
