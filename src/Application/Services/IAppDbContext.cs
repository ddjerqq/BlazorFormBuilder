using Domain.Aggregates;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Services;

public interface IAppDbContext : IDisposable
{
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
    public DbSet<BaseComponentChoice> Components => Set<BaseComponentChoice>();
    public DbSet<UserForm> Forms => Set<UserForm>();
    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    public Task<int> SaveChangesAsync(CancellationToken ct = default);
}