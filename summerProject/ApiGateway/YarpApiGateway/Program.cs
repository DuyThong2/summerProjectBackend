using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using YarpApiGatway.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SwaggerSourceSetting>(
    builder.Configuration.GetSection(SwaggerSourceSetting.SectionName)
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// 2. Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Add Reverse Proxy
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// 4. Add Rate Limiter
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
    });
});

// OpenTelemetry setup
var serviceName = "yarp-gateway";
var serviceVersion = "1.0.0";

builder.Services.AddOpenTelemetry()
    .WithTracing(tracer =>
    {
        tracer
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName, serviceVersion))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter(opt => opt.Endpoint = new Uri("http://otel-collector:4317"));
    })
    .WithMetrics(metrics =>
    {
        metrics
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName, serviceVersion))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddOtlpExporter(opt => opt.Endpoint = new Uri("http://otel-collector:4317"));
    });

var app = builder.Build();
app.MapGet("/",() => "hello world");

// ---- Middleware pipeline ----
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    var swaggerOptions = app.Services.GetRequiredService<IOptions<SwaggerSourceSetting>>().Value;

    c.SwaggerEndpoint(swaggerOptions.Catalog, "Catalog API");
    c.SwaggerEndpoint(swaggerOptions.Scheduling, "Scheduling API");
    c.SwaggerEndpoint(swaggerOptions.Ordering, "Ordering API");
});

app.UseCors("AllowAll");
// (B) Rate Limiter
app.UseRateLimiter();

// (C) Reverse Proxy
app.MapReverseProxy();

app.Run();
