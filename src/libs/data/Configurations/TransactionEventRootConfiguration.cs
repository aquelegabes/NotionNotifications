using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotionNotifications.Domain.Entities;

namespace NotionNotifications.Data.Configurations
{
    public class TransactionEventRootConfiguration : IEntityTypeConfiguration<TransactionEventRoot>
    {
        public void Configure(EntityTypeBuilder<TransactionEventRoot> builder)
        {
            builder.ToTable(nameof(TransactionEventRoot));

            builder.HasKey(t => t.Id);

            builder.Property(t => t.EventDate);

            builder.Property(t => t.NewObjectAsJsonString).HasColumnType("text");

            builder.Property(t => t.OldObjectAsJsonString).HasColumnType("text");

            builder.Property(t => t.Description).HasColumnType("text");

            builder.Property(t => t.TransactionType).HasColumnType("int").IsRequired();
        }
    }
}
