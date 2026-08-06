using MediaBoomService.Domain;
using MediaBoomService.Domain.Compositions;
using MediaBoomService.Domain.Compositions.ValueObjects;
using MediaBoomService.Domain.Users;
using MediaBoomService.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBoomService.Infrastructure.Postgres.Configurations;

public class CompositionConfiguration : IEntityTypeConfiguration<Composition>
{
    public void Configure(EntityTypeBuilder<Composition> builder)
    {
        builder.ToTable("compositions");

        builder.HasKey(c => c.Id).HasName("pk_compositions");

        builder.Property(c => c.Id)
            .HasConversion(c => c.Value, compositionId => CompositionId.FromValue(compositionId))
            .HasColumnName("id")
            .IsRequired();

        builder.Property(c => c.UploadedByUserId)
            .HasConversion(c => c.Value, uploadedByUserId => UserId.FromValue(uploadedByUserId))
            .HasColumnName("uploaded_by_user_id")
            .IsRequired();

        builder.Property(c => c.FileId)
            .HasColumnName("file_id")
            .IsRequired();

        builder.Property(c => c.Title)
            .HasConversion(title => title.Value, value => Title.Create(value).Value)
            .HasColumnName("title")
            .HasMaxLength(LengthConstants.LENGTH200)
            .IsRequired();

        builder.Property(c => c.ArtistName)
            .HasConversion(artistName => artistName.Value, value => ArtistName.Create(value).Value)
            .HasColumnName("artist_name")
            .HasMaxLength(LengthConstants.LENGTH150)
            .IsRequired();

        builder.Property(c => c.PublishedAtUtc)
            .HasColumnName("published_at_utc")
            .IsRequired();

        // Each composition is uploaded by one user; 
        // a user may upload many compositions
        // A user cannot be deleted while their compositions still exist.
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.UploadedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
