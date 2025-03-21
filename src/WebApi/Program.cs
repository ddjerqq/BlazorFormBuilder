using System.Text.Json;
using Application.Common.Behaviours;
using Domain.Common;
using dotenv.net;
using FluentValidation;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

// load env vars
DotEnv.Fluent().WithProbeForEnv(6).Load();

var builder = WebApplication.CreateBuilder(args);

#region application services

builder.Services.AddMemoryCache();
builder.Services.AddValidatorsFromAssembly(Application.Application.Assembly, includeInternalTypes: true);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Application.Application.Assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
});

#endregion

#region Infrastructure services

builder.Services.ConfigurePersistence();

#endregion

#region web api services

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors(opt => opt.AddDefaultPolicy(cors =>
{
    cors.AllowAnyMethod();
    cors.AllowAnyOrigin();
    cors.AllowAnyHeader();
}));

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new BaseComponentChoiceConverter());
    options.JsonSerializerOptions.Converters.Add(new Vec2StringJsonConverter());
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});

#endregion

var app = builder.Build();

// auto migrate pending migrations
await app.ApplyPendingMigrations();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseRouting();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

#pragma warning disable ASP0014, It does not work without endpoints, so make the error shut up
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});
#pragma warning restore ASP0014

app.Run();