using Scheduling.API.Repository.Impl;
using Scheduling.API.Repository;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IScheduleTemplateRepository, ScheduleTemplateRepository>();
builder.Services.AddScoped<IScheduleInstanceRepository, ScheduleInstanceRepository>();
builder.Services.AddScoped<IScheduleTemplateDetailRepository, ScheduleTemplateDetailRepository>();
builder.Services.AddScoped<IScheduleInstanceDetailRepository, ScheduleInstanceDetailRepository>();


app.MapGet("/", () => "Hello World!");

app.Run();
