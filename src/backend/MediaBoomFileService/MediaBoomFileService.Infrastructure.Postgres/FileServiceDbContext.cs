using MediaBoomFileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediaBoomFileService.Infrastructure.Postgres;

/// <summary>
/// File for responsible for work with db context.
/// </summary>
public class FileServiceDbContext : DbContext
{
    private readonly string _connectionString;

    public FileServiceDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_connectionString)
            .EnableDetailedErrors()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FileServiceDbContext).Assembly);
    }

    public DbSet<MediaAsset> MediaAssets => Set<MediaAsset>();
    public DbSet<AudioAsset> VideoAssets => Set<AudioAsset>();
    public DbSet<AudioProcess> VideoProcess => Set<AudioProcess>();
}