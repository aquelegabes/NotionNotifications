using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotionNotifications.Domain.Entities;

namespace NotionNotifications.Data.Configurations
{
    public class NotificationRootConfiguration : IEntityTypeConfiguration<NotificationRoot>
    {
        public void Configure(EntityTypeBuilder<NotificationRoot> builder)
        {
            builder.ToTable(nameof(NotificationRoot));

            builder.HasKey(t => t.Id);

            builder.Property(t => t.NotionIdProperty).HasColumnType("int");

            builder.Property(t => t.IntegrationId).HasColumnType("text");

            builder.Property(t => t.AlreadyNotified).HasColumnType("int");

            builder.Property(t => t.Title).HasColumnType("text");

            builder.Property(t => t.CreatedAt);

            builder.Property(t => t.LastUpdatedAt);

            builder.Property(t => t.EventDate);

            builder.Property(t => t.Occurence).HasColumnType("int");
        }
    }
}
