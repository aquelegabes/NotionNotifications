using Microsoft.EntityFrameworkCore;
using NotionNotifications.Domain.Entities;

namespace NotionNotifications.Data
{
    public class NotionNotificationsContext : DbContext
    {
        public DbSet<NotificationRoot> NotificationRoot { get; set; }

        public DbSet<TransactionEventRoot> TransactionEventRoots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder opts)
        {
            opts.UseSqlite("DataSource=app.db;Cache=Shared");
            base.OnConfiguring(opts);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotionNotificationsContext).Assembly);
        }
    }
}
