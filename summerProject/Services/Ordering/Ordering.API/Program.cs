using System.Diagnostics;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

// Tracing ONLY (no metrics)
var serviceName = "ordering-api";
var serviceVersion = "1.0.0";

//builder.Services.AddOpenTele ssss metry()
//    .WithTracing(tracer =>
//    {
//        tracer
//            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName, serviceVersion))
//            .AddAspNetCoreInstrumentation()
//            .AddHttpClientInstrumentation()
//                        .AddConsoleExporter()

//            .AddOtlpExporter(opt =>
//            {
//                opt.Endpoint = new Uri("http://tempo:4318");
//                opt.Protocol = OtlpExportProtocol.HttpProtobuf;
//            });
//    });

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

app.MapGet("/", () => "hello worlds");

app.UseApiServices(); // Custom middleware and endpoint config

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
    });

    await app.InitialiseDatabaseAsync();
}

app.Run();
