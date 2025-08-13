using System.Diagnostics;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using YarpApiGatway.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SwaggerSourceSetting>(
    builder.Configuration.GetSection(SwaggerSourceSetting.SectionName)
);

// CORS sss



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Reverse Proxy (YARP)
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Rate Limiter
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", limiter =>
    {
        limiter.Window = TimeSpan.FromSeconds(10);
        limiter.PermitLimit = 5;
    });
});

// OpenTelemetry
var serviceName = "yarp-gateway";
var serviceVersion = "1.0.0";

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
        tracerProviderBuilder
            .AddSource(new ActivitySource(serviceName).Name)
            .ConfigureResource(resource => resource
                .AddService(serviceName))
            .AddAspNetCoreInstrumentation()
            .AddConsoleExporter()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri("http://otel-collector:4317");
                options.Protocol = OtlpExportProtocol.Grpc;
            }));

var app = builder.Build();



// Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    var swaggerOptions = app.Services.GetRequiredService<IOptions<SwaggerSourceSetting>>().Value;

    c.SwaggerEndpoint(swaggerOptions.Catalog, "Catalog API");
    c.SwaggerEndpoint(swaggerOptions.Scheduling, "Scheduling API");
    c.SwaggerEndpoint(swaggerOptions.Ordering, "Ordering API");
});

// CORS
app.UseCors("AllowAll");

// Rate Limiting
app.UseRateLimiter();

// Routing + Metrics endpoint
app.UseRouting();

app.UseMiddleware<TracingHeaderMiddleware>();

app.UseHttpMetrics();


app.UseEndpoints(endpoints =>
{
    endpoints.MapReverseProxy();
    endpoints.MapMetrics(); 
});

// Optional: root test
app.MapGet("/", () => "hello world");

app.Run();
