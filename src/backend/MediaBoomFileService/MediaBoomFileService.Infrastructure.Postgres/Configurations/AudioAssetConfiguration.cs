using MediaBoomFileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBoomFileService.Infrastructure.Postgres.Configurations;

public class AudioAssetConfiguration : IEntityTypeConfiguration<AudioAsset>
{
    public void Configure(EntityTypeBuilder<AudioAsset> builder)
    {
        builder
            .Property(x => x.DurationMs)
            .HasColumnName("duration_ms");

        builder
            .Property(x => x.SourceContainer)
            .HasColumnName("source_container");

        builder
            .Property(x => x.SourceCodec)
            .HasColumnName("source_codec");

        builder.OwnsOne(x => x.HlsManifestObjectKey, hb =>
        {
            hb.Property(k => k.Bucket).HasColumnName("hls_manifest_object_key_bucket");
            hb.Property(k => k.Prefix).HasColumnName("hls_manifest_object_key_prefix");
            hb.Property(k => k.Key).HasColumnName("hls_manifest_object_key_key");
            hb.Property(k => k.Value).HasColumnName("hls_manifest_object_key_value");
            hb.Property(k => k.FullPath).HasColumnName("hls_manifest_object_key_full_path");
        });
    }
}