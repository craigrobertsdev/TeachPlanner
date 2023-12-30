namespace TeachPlanner.Shared.Common.Exceptions;

public abstract class BaseException : Exception {
    public BaseException(string message, int statusCode, string type) : base(message) {
        StatusCode = statusCode;
        Type = type;
    }

    public int StatusCode { get; set; }
    public string Type { get; set; }
}