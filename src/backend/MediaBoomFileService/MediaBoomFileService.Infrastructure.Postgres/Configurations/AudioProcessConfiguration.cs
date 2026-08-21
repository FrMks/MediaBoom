using MediaBoomFileService.Domain.Entities;
using MediaBoomFileService.Domain.MediaProcessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBoomFileService.Infrastructure.Postgres.Configurations;

public class AudioProcessConfiguration : IEntityTypeConfiguration<AudioProcess>
{
    public void Configure(EntityTypeBuilder<AudioProcess> builder)
    {
        builder.ToTable("audio_processes", "files");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.AudioAssetId).HasColumnName("audio_asset_id");
        builder.Property(x => x.Status).HasConversion<string>().HasColumnName("status");
        builder.Property(x => x.ProgressPercentage).HasColumnName("progress_percentage");
        builder.Property(x => x.ErrorMessage).HasColumnName("error_message");
        builder.Property(x => x.StartedAt).HasColumnName("started_at");
        builder.Property(x => x.CompletedAt).HasColumnName("completed_at");
        builder.Property(x => x.IsCriticalError).HasColumnName("is_critical_error");
        builder.Property(x => x.RetryCount).HasColumnName("retry_count");
        builder.Property(x => x.MaxRetries).HasColumnName("max_retries");
        builder.Property(x => x.NextRetryAt).HasColumnName("next_retry_at");

        builder
            .HasOne<AudioAsset>()
            .WithMany()
            .HasForeignKey(x => x.AudioAssetId)
            .OnDelete(DeleteBehavior.Cascade);

        // ProcessingStep является частью AudioProcess и не существует отдельно
        // от своего процесса, поэтому используется OwnsMany вместо HasMany.
        // При удалении AudioProcess связанные шаги также удаляются.
        builder.OwnsMany(vp => vp.Steps, sb =>
        {
            sb.ToTable("processing_steps", "files");
            sb.HasKey(s => s.Id);

            sb.Property(x => x.Id).HasColumnName("id");
            sb.Property(x => x.StepType).HasConversion<string>().HasColumnName("step_type");
            sb.Property(x => x.Status).HasConversion<string>().HasColumnName("status");
            sb.Property(x => x.Order).HasColumnName("order");
            sb.Property(x => x.Weight).HasColumnName("weight");
            sb.Property(x => x.ResultData).HasColumnName("result_data").HasColumnType("jsonb");
            sb.Property(x => x.ErrorMessage).HasColumnName("error_message");
            sb.Property(x => x.StartedAt).HasColumnName("started_at");
            sb.Property(x => x.CompletedAt).HasColumnName("completed_at");

            // Создаёт shadow property: поле существует в EF-модели,
            // но не объявлено отдельным свойством в классе ProcessingStep.
            // Оно связывает каждую запись шага с родительским AudioProcess.
            sb.WithOwner().HasForeignKey("AudioProcessId");
            sb.Property<Guid>("AudioProcessId").HasColumnName("audio_process_id");
            sb.HasIndex("AudioProcessId").HasDatabaseName("ix_processing_steps_audio_process_id");

            sb.HasIndex(s => new { s.StepType }).HasDatabaseName("ix_processing_steps_step_type");
            sb.HasIndex(s => new { s.Status }).HasDatabaseName("ix_processing_steps_status");
        });
    }
}
