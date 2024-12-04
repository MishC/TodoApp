using Serilog;

using System.Text.Json;
using System.Net;


public class ExceptionHandlingMiddleware : IExceptionHandler
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
            
        }
        catch (ValidationException ex)
        {
            Log.Error(ex, "Validation exception occurred.");
            await WriteJsonResponseAsync(context, (int)HttpStatusCode.BadRequest, $"Bad request: {ex.Message}");
        }
        catch (NotFoundException ex)
        {
            Log.Warning(ex, "Resource not found.");
            await WriteJsonResponseAsync(context, (int)HttpStatusCode.NotFound, $"Not found: {ex.Message}");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Internal Server Error.");
            await WriteJsonResponseAsync(context, (int)HttpStatusCode.InternalServerError, $"Internal Server Error: {ex.Message}");
        }
        finally
        {
            Log.Information("HTTP {Method} {Url} responded {StatusCode}",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode);
        }
    }

    public async Task WriteJsonResponseAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var errorResponse = new
        {
            StatusCode = statusCode,
            Message = message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}
