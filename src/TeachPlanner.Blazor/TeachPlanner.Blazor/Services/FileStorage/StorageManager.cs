using TeachPlanner.Blazor.Common.Interfaces.Services;

namespace TeachPlanner.Blazor.Services.FileStorage;

public class StorageManager : IStorageManager {
    public async Task<string> UploadResource(Stream file, CancellationToken cancellationToken) {
        return await Task.FromResult("https://google.com.au");
    }
}
