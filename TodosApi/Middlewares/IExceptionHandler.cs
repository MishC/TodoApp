using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public interface IExceptionHandler
{

    Task InvokeAsync(HttpContext context);
	Task WriteJsonResponseAsync(HttpContext context, int statusCode, string message);

}
