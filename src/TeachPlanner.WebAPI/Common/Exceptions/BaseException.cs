namespace TeachPlanner.Api.Common.Exceptions;
public class BaseException : Exception
{
    public int StatusCode { get; set; }
    public string Type { get; set; }
    public BaseException(string message, int statusCode, string type) : base(message)
    {
        StatusCode = statusCode;
        Type = type;
    }
}
