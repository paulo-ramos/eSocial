using Microsoft.AspNetCore.Http;

namespace Ramos.eSocial.S1000.Infrastructure.Middleware;

public class RequestIdMiddleware
{
    private readonly RequestDelegate _next;

    public RequestIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Items["ProcessKey"] = Guid.NewGuid().ToString();
        context.Items["ProcessDate"] = DateTime.UtcNow;
        await _next(context);
    }

}