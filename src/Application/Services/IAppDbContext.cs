using Application.Models;
using Domain.Aggregates;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Services;

public interface IAppDbContext : IDisposable
{
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
    public DbSet<BaseComponentChoice> Components => Set<BaseComponentChoice>();
    public DbSet<UserForm> Forms => Set<UserForm>();
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    public Task<int> SaveChangesAsync(CancellationToken ct = default);
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default);
}