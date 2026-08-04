using MediaBoomService.Domain;
using MediaBoomService.Domain.Users;
using MediaBoomService.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBoomService.Infrastructure.Postgres.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id).HasName("pk_users");

        builder.Property(u => u.Id)
            .HasConversion(u => u.Value, userId => UserId.FromValue(userId))
            .HasColumnName("id")
            .IsRequired();

        builder.Property(u => u.Email)
            .HasConversion(e => e.Value, email => Email.Create(email).Value)
            .HasColumnName("email")
            .HasMaxLength(LengthConstants.LENGTH254)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("ux_users_email");

        builder.Property(u => u.LogoUrl)
            .HasColumnName("logo_url")
            .HasMaxLength(LengthConstants.LENGTH2048)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(LengthConstants.LENGTH512)
            .IsRequired();

        builder.Property(u => u.Username)
            .HasColumnName("username")
            .HasMaxLength(LengthConstants.LENGTH50)
            .IsRequired();

        builder.Property(u => u.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();
    }
}
