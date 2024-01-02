using System.Net.Http.Json;
using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Common.Interfaces.Services;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Blazor.Client.Services;

public class TeacherService : ITeacherService {
    private readonly HttpClient _httpClient;

    public TeacherService(HttpClient httpClient) {
        _httpClient = httpClient;
    }
    public async Task<Guid?> GetTeacherId(string userId, CancellationToken cancellationToken) {
        Console.WriteLine("Calling GetTeacherId from Client");
        var teacherId = await _httpClient.GetFromJsonAsync<Guid?>($"/api/user/{userId}/teacher-id", cancellationToken);

        if (teacherId == null) {
            throw new TeacherNotFoundException();
        }

        return teacherId.Value;
    }
}
