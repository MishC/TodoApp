public class LoggingMiddleware
{
    // Variable som holder referansen til neste middleware i serien
    private readonly RequestDelegate _next;

    // Konstruktør som initialiserer LoggingMiddleware med RequestDelegate
    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Metode som blir kalt for hver http-request 
    public async Task InvokeAsync(HttpContext context)
    {
        // Logger Detaljene for den innkommende requesten
        Console.WriteLine($"[{DateTime.Now}] Request: {context.Request.Method} {context.Request.Path}");
        // Sender requesten videre til neste middleware 
        await _next(context);

        // Logger detaljene for svaret eller response etter at neste middleware (Controller) er ferdig
        Console.WriteLine($"[{DateTime.Now}] Response: {context.Response.StatusCode}");


    }

}
