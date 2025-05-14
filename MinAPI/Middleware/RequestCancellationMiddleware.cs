public class RequestCancellationMiddleware
{
    private readonly RequestDelegate _next;

    public RequestCancellationMiddleware(RequestDelegate next, ILogger<RequestCancellationMiddleware> logger)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
            context.Response.StatusCode = 499;
        }
    }
}
