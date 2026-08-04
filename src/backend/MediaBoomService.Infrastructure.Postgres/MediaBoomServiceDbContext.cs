using System.Data.Common;
using MediaBoomService.Domain.Compositions;
using MediaBoomService.Domain.UserFavoriteCompositions;
using MediaBoomService.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Database;

namespace MediaBoomService.Infrastructure.Postgres;

public sealed class MediaBoomServiceDbContext : DbContext, IDbConnectionFactory
{
    private readonly string _connectionString;

    public MediaBoomServiceDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString); // Use Npgsql like a database
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MediaBoomServiceDbContext).Assembly);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Composition> Compositions => Set<Composition>();
    public DbSet<UserFavoriteComposition> UserFavoriteCompositions => Set<UserFavoriteComposition>();

    public DbConnection GetDbConnection() => Database.GetDbConnection();
}