using MediaBoomService.Domain.Compositions;
using MediaBoomService.Domain.Compositions.ValueObjects;
using MediaBoomService.Domain.UserFavoriteCompositions;
using MediaBoomService.Domain.UserFavoriteCompositions.ValueObjects;
using MediaBoomService.Domain.Users;
using MediaBoomService.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBoomService.Infrastructure.Postgres.Configurations;

public class UserFavoriteCompositionConfiguration : IEntityTypeConfiguration<UserFavoriteComposition>
{
    public void Configure(EntityTypeBuilder<UserFavoriteComposition> builder)
    {
        builder.ToTable("user_favorite_compositions");

        builder.HasKey(favorite => favorite.UserFavoriteCompositionId)
            .HasName("pk_user_favorite_compositions");

        builder.Property(favorite => favorite.UserFavoriteCompositionId)
            .HasConversion(id => id.Value, value => UserFavoriteCompositionId.FromValue(value))
            .HasColumnName("id")
            .IsRequired();

        builder.Property(favorite => favorite.UserId)
            .HasConversion(id => id.Value, value => UserId.FromValue(value))
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(favorite => favorite.CompositionId)
            .HasConversion(id => id.Value, value => CompositionId.FromValue(value))
            .HasColumnName("composition_id")
            .IsRequired();

        // Prevent the same user can add the same composition
        builder.HasIndex(favorite => new { favorite.UserId, favorite.CompositionId })
            .IsUnique()
            .HasDatabaseName("ux_user_favorite_compositions_user_id_composition_id");

        // A user may have mane favorite records.
        // Deleting the user deletes all their favorite records
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(favorite => favorite.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // A composition may be favorited by many users.
        // Deleting the composition deletes all favorite record referencing it
        builder.HasOne<Composition>()
            .WithMany()
            .HasForeignKey(favorite => favorite.CompositionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
