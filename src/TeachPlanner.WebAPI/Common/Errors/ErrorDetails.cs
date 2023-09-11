using System.Text.Json;

namespace TeachPlanner.Api.Common.Errors;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = null!;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
