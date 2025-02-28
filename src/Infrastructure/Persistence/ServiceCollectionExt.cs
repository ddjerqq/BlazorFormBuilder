using Application.Services;
using Domain.Common;
using Microsoft.AspNetCore.Builder;
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

    public static async Task ApplyPendingMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
        {
            var migrations = await dbContext.Database.GetPendingMigrationsAsync();
            Console.WriteLine("Applying migrations: {0}", string.Join(", ", migrations));
            await dbContext.Database.MigrateAsync();
        }

        Console.WriteLine("All migrations applied");

        await dbContext.SaveChangesAsync();
    }
}