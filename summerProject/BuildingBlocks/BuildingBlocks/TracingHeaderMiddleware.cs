using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class TracingHeaderMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TracingHeaderMiddleware> _logger;

    public TracingHeaderMiddleware(RequestDelegate next, ILogger<TracingHeaderMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {


        var activity = Activity.Current;

        if (activity != null)
        {
            var traceId = activity.TraceId.ToString();
            var spanId = activity.SpanId.ToString();

            // Log to console or file
            _logger.LogInformation("[Trace] {Method} {Path} => TraceId: {TraceId}, SpanId: {SpanId}",
                context.Request.Method,
                context.Request.Path,
                traceId,
                spanId);

            // Add trace info to response headers for easy debugging
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["trace-id"] = traceId;
                context.Response.Headers["span-id"] = spanId;
                context.Response.Headers["traceparent"] = $"00-{traceId}-{spanId}-01";
                return Task.CompletedTask;
            });
        }
        else
        {
            _logger.LogWarning("[Trace] No Activity found for {Method} {Path}",
                context.Request.Method, context.Request.Path);
        }

        await _next(context);
    }
}
