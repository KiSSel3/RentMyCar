using System.Net;
using CarManagementService.Application.Exceptions;
using Newtonsoft.Json;

namespace CarManagementService.Presentation.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    
    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;
        
        switch (exception)
        {
            case EntityAlreadyExistsException:
                _logger.LogWarning("Entity already exists: {Message}", exception.Message);
                response.StatusCode = (int)HttpStatusCode.Conflict;
                break;
            
            case EntityNotFoundException:
                _logger.LogWarning("Entity not found: {Message}", exception.Message);
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            
            case FormatException:
                _logger.LogWarning("Format exception: {Message}", exception.Message);
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            
            case ValidationException:
                _logger.LogWarning("Validation exception: {Message}", exception.Message);
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            
            case UnauthorizedAccessException:
                _logger.LogWarning("Unauthorized access: {Message}", exception.Message);
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            
            default:
                _logger.LogError(exception, "An unhandled exception occurred");
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var result = JsonConvert.SerializeObject(new 
        {
            error = exception.Message,
            details = exception.StackTrace
        });
        
        _logger.LogDebug("Error response: {StatusCode} - {Result}", response.StatusCode, result);

        return context.Response.WriteAsync(result);
    }
}