using Application.Services;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public static class ServiceCollectionExt
{
    public static void ConfigurePersistence(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(builder =>
        {
            builder.EnableDetailedErrors();
            builder.EnableSensitiveDataLogging();

            var dbFilePath = "DB__PATH".FromEnvRequired();
            var dbDirectory = Path.GetDirectoryName(dbFilePath)!;

            var directory = new DirectoryInfo(dbDirectory);
            if (!directory.Exists)
                directory.Create();

            builder.UseSqlite($"Data Source={dbFilePath}");
        });

        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
    }
}