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

// 6. Use Swagger 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 7. Map Controllers
app.MapControllers();

// 8. Seed Database
await app.InitialiseDatabaseAsync();

// 9. Run
app.Run();

