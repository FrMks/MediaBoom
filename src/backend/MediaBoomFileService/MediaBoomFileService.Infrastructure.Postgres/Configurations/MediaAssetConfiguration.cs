using MediaBoomFileService.Domain.Entities;
using MediaBoomFileService.Domain.Enums;
using MediaBoomFileService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBoomFileService.Infrastructure.Postgres.Configurations;

public class MediaAssetConfiguration : IEntityTypeConfiguration<MediaAsset>
{
    public void Configure(EntityTypeBuilder<MediaAsset> builder)
    {
        // Используем стратегию TPT (Table Per Type) для иерархии MediaAsset.
        // Общие свойства базового класса MediaAsset хранятся в таблице media_assets,
        // а свойства конкретного типа AudioAsset — в отдельной таблице audio_assets.
        // Обе таблицы связываются по одному и тому же Id: audio_assets.id является
        // одновременно первичным ключом и внешним ключом на media_assets.id.
        builder.UseTptMappingStrategy();

        // Базовая таблица для всех медиа-ассетов находится в схеме files.
        builder.ToTable("media_assets", "files");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.AssetType)
            .HasConversion<string>()
            .HasColumnName("asset_type")
            .HasMaxLength(LengthConstants.LENGTH32)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasColumnName("status")
            .HasMaxLength(LengthConstants.LENGTH32)
            .IsRequired();

        builder.Property(x => x.FailureReason)
            .HasColumnName("failure_reason")
            .HasColumnType("text");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(x => x.ReadyAt)
            .HasColumnName("ready_at")
            .HasColumnType("timestamp with time zone");

        builder.OwnsOne(m => m.MediaData, mb =>
        {
            mb.OwnsOne(md => md.FileName, fb =>
            {
                fb.Property(x => x.Extension)
                    .HasColumnName("source_file_extension");
                fb.Property(x => x.Name)
                    .HasColumnName("source_file_name");
            });

            mb.OwnsOne(md => md.ContentType, ctb =>
            {
                ctb.Property(x => x.Category)
                    .HasConversion<string>()
                    .HasColumnName("source_media_type");
                ctb.Property(x => x.Value)
                    .HasColumnName("source_content_type");
            });

            mb.Property(md => md.SizeBytes)
                .HasColumnName("source_size_bytes");
            mb.Property(md => md.ExpectedChunksCount)
                .HasColumnName("expected_chunks_count");
        });

        builder.OwnsOne(m => m.SourceObjectKey, kb =>
        {
            kb.Property(x => x.Bucket).HasColumnName("source_object_key_bucket");
            kb.Property(x => x.Prefix).HasColumnName("source_object_key_prefix");
            kb.Property(x => x.Key).HasColumnName("source_object_key_key");
            kb.Property(x => x.Value).HasColumnName("source_object_key_value");
            kb.Property(x => x.FullPath).HasColumnName("source_object_key_full_path");
        });

        builder.HasIndex(x => new
        {
            x.Status,
            x.CreatedAt,
        });

        // Текущий builder настраивает базовый тип MediaAsset, поэтому AudioAsset
        // настраиваем через метаданные модели отдельно.
        // В результате специфичные свойства AudioAsset будут записываться в
        // files.audio_assets, а общие свойства — в files.media_assets.
        builder.Metadata.Model
            .FindEntityType(typeof(AudioAsset))?
            .SetTableName("audio_assets");

        // Явно указываем схему для таблицы производного типа AudioAsset.
        builder.Metadata.Model
            .FindEntityType(typeof(AudioAsset))?
            .SetSchema("files");
    }
}
