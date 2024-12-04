public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Metode som blir kalt for hver http-request 
    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"[{DateTime.Now}] Request: {context.Request.Method} {context.Request.Path}");
        await _next(context);

        Console.WriteLine($"[{DateTime.Now}] Response: {context.Response.StatusCode}");


    }

}
