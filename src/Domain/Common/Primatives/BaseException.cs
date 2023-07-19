using System.Net;

namespace Domain.Common.Primatives;

public abstract class BaseException : Exception
{
    public HttpStatusCode Status { get; }
    public string Title { get; }
    public string Type { get; }
    public string Detail { get; }

    public BaseException(string message, HttpStatusCode status, string title, string type, string detail) : base(message)
    {
        Status = status;
        Title = title;
        Type = type;
        Detail = detail;
    }

}
