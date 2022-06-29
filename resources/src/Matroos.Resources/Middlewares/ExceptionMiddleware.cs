using System.Text.Json;

using Microsoft.AspNetCore.Http;

namespace Matroos.Resources.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _request;

    public ExceptionMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 400;
            string json = JsonSerializer.Serialize(new { Msg = ex.Message });
            await context.Response.WriteAsync(json);
        }
    }
}
