using MediaBoomService.Domain;
using MediaBoomService.Domain.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaBoomService.Infrastructure.Postgres.Configurations;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");

        builder.HasKey(message => message.Id)
            .HasName("pk_outbox_messages");

        builder.Property(message => message.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(message => message.MessageType)
            .HasColumnName("message_type")
            .HasMaxLength(LengthConstants.LENGTH200)
            .IsRequired();

        builder.Property(message => message.Payload)
            .HasColumnName("payload")
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(message => message.OccurredAtUtc)
            .HasColumnName("occurred_at_utc")
            .IsRequired();

        builder.Property(message => message.ProcessedAtUtc)
            .HasColumnName("processed_at_utc");

        builder.Property(message => message.RetryCount)
            .HasColumnName("retry_count")
            .IsRequired();

        builder.HasIndex(message => message.ProcessedAtUtc)
            .HasDatabaseName("ix_outbox_messages_processed_at_utc");
    }
}
