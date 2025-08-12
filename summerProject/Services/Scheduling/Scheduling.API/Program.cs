using Microsoft.EntityFrameworkCore;
using Scheduling.API.Repository;
using Scheduling.API.Repository.Impl;
using Scheduling.Infrastructure.Data;
using Scheduling.Infrastructure.Data.Extensions;
using MediatR;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Scheduling.API.Data;
using BuildingBlocks.Behaviors;
using System.Reflection;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry.Metrics;
using Prometheus;
using OpenTelemetry.Exporter;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SchedulingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IScheduleDbContext, SchedulingDbContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IScheduleTemplateRepository, ScheduleTemplateRepository>();


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    
});


builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

// 4. Add Controllers
builder.Services.AddControllers();

// 5. Add Swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// OpenTelemetry setup
var serviceName = "scheduling-api";
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

// 6. Use Swagger 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 7. Map Controllers
app.UseHttpMetrics();
app.UseRouting(); 


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapMetrics(); 
});

// 8. Seed Database
await app.InitialiseDatabaseAsync();

// 9. Run
app.Run();

