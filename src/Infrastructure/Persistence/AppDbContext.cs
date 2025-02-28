using System.Reflection;
using Application.Services;
using Domain.Aggregates;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.ValueConverters;

namespace Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<UserForm> Forms => Set<UserForm>();

    public DbSet<BaseComponentChoice> BaseComponentChoices => Set<BaseComponentChoice>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.Load(nameof(Infrastructure)));
        base.OnModelCreating(builder);
        SnakeCaseRename(builder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder
            .Properties<DateTime>()
            .HaveConversion<DateTimeToUtcDateTimeValueConverter>();

        base.ConfigureConventions(builder);
    }


    private static void SnakeCaseRename(ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            var entityTableName = entity.GetTableName()!
                .Replace("AspNet", string.Empty)
                .TrimEnd('s')
                .ToSnakeCase();

            entity.SetTableName(entityTableName);

            foreach (var property in entity.GetProperties())
                property.SetColumnName(property.GetColumnName().ToSnakeCase());

            foreach (var key in entity.GetKeys())
                key.SetName(key.GetName()!.ToSnakeCase());

            foreach (var key in entity.GetForeignKeys())
                key.SetConstraintName(key.GetConstraintName()!.ToSnakeCase());

            foreach (var index in entity.GetIndexes())
                index.SetDatabaseName(index.GetDatabaseName()!.ToSnakeCase());
        }
    }
}