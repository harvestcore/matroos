using System.Text.Json;

using Microsoft.AspNetCore.Http;

namespace Matroos.Resources.Exceptions;

/// <summary>
/// Exception middleware. Avoids sending ugly exceptions to the user.
/// </summary>
public class ExceptionMiddleware
{
    /// <summary>
    /// The request.
    /// </summary>
    private readonly RequestDelegate _request;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="request">The request.</param>
    public ExceptionMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    /// <summary>
    /// Invokes the request and catches rised exceptions.
    /// </summary>
    /// <param name="context">The context.</param>
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
