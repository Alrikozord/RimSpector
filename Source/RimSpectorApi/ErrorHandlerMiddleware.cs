namespace RimSpectorApi;

using System.Net;
using System.Text.Json;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var response = context.Response;
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            switch (error)
            {
                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Redirect("/errors/404");
                    break;
                case Exception e:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Redirect("/errors/500");
                    break;
            }           
        }        
    }
}