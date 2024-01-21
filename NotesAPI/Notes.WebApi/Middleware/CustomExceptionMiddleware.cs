using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Notes.Application.Exceptions;

namespace Notes.WebApi.Middleware;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var resultDto = (ExceptionHandlerResultDto)Handle(exception as dynamic);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)resultDto!.Code;
        
        return context.Response.WriteAsync(resultDto.Result);
    }

    private static ExceptionHandlerResultDto Handle(ValidationException validationException) 
        => new ()
        {
            Code = HttpStatusCode.BadRequest,
            Result = JsonSerializer.Serialize(validationException.ValidationResult)
        };
    
    private static ExceptionHandlerResultDto Handle(NotFoundException notFoundException) 
        => new ()
        {
            Code = HttpStatusCode.NotFound,
            Result = JsonSerializer.Serialize(notFoundException.Message)
        };
    
    private static ExceptionHandlerResultDto Handle(Exception exception) 
        => new ()
        {
            Code = HttpStatusCode.InternalServerError,
            Result = JsonSerializer.Serialize(exception.Message)
        };
    
    
}
