namespace TeachPlanner.Shared.Common.Interfaces.Services;

public interface IStorageManager {
    Task<string> UploadResource(Stream file, CancellationToken cancellationToken);
}
