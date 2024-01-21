using System.Net;

namespace Notes.WebApi.Middleware;

public class ExceptionHandlerResultDto
{
    public string Result { get; set; } = string.Empty;

    public HttpStatusCode Code { get; set; }
}