using MediaBoomFileService.Domain.Entities;
using MediaBoomFileService.Domain.MediaProcessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediaBoomFileService.Infrastructure.Postgres;

/// <summary>
/// Provides the Entity Framework Core database context for the FileService database.
/// </summary>
public class FileServiceDbContext : DbContext
{
    private readonly string _connectionString;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileServiceDbContext"/> class.
    /// </summary>
    /// <param name="connectionString">The PostgreSQL connection string.</param>
    public FileServiceDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// Configures the database provider, connection and logging options.
    /// </summary>
    /// <param name="optionsBuilder">The builder used to configure the context.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_connectionString)
            .EnableDetailedErrors()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }

    /// <summary>
    /// Applies entity configurations from the Infrastructure.Postgres assembly.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure the EF Core model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FileServiceDbContext).Assembly);
    }

    /// <summary>
    /// Gets the set of media assets.
    /// </summary>
    public DbSet<MediaAsset> MediaAssets => Set<MediaAsset>();

    /// <summary>
    /// Gets the set of audio assets.
    /// </summary>
    public DbSet<AudioAsset> AudioAssets => Set<AudioAsset>();

    /// <summary>
    /// Gets the set of audio processing processes.
    /// </summary>
    public DbSet<AudioProcess> AudioProcesses => Set<AudioProcess>();
}
