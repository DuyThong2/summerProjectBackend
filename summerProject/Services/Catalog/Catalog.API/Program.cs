using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Catalog.API.Data;
using Catalog.API.Data.Interfaces;
using Catalog.API.Mapper;
using Catalog.API.Repositories;
using Catalog.API.Repositories.impl;
using Catalog.API.Services;
using Catalog.API.Services.impl;
using Catalog.API.Settings;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// 1. Add MediatR
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// 2. Add Controllers
builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg =>{ }, typeof(CustomMapperProfile).Assembly);


// 3. Add MongoDB Settings
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
builder.Services.Configure<CatalogDatabaseSettings>(
    builder.Configuration.GetSection(nameof(CatalogDatabaseSettings)));
builder.Services.AddSingleton<ICatalogDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value);

// 4. MongoDB Context
builder.Services.AddTransient<ICatalogContext, CatalogContext>();


// 5. Register Repositories
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepositoryBase<>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IPackageIngredientRepository, PackageIngredientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// 6. Register Services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IPackageIngredientService, PackageIngredientService>();
builder.Services.AddScoped<IProductService, ProductService>();

//excception
builder.Services.AddExceptionHandler<CustomExceptionHandler>();




// 7. Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog API", Version = "v1" });
});

// OpenTelemetry setup
var serviceName = "ordering-api";
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
var env = builder.Environment;

app.MapGet("/", () => "hello worlds");

// 8. Middleware pipeline
if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
    });
}

app.UseExceptionHandler(options => { });

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
