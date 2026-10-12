using Microsoft.EntityFrameworkCore;
using PlasticLicensingApp.Models;
using PlasticLicensingApp.Models.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlasticLicensingApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<ProductParameter> ProductParameters { get; set; }
        public DbSet<PlasticLicensingApp.Models.File> Files { get; set; }
        public DbSet<ProductResponse> ProductResponses { get; set; }
        public DbSet<StatusHistoryChange> StatusHistoryChanges { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure 4-digit License Number padding
            modelBuilder.Entity<License>()
                .Property(l => l.LicenseNumber)
                .HasComputedColumnSql("RIGHT('0000' + CAST(ID AS VARCHAR), 4)");

            // Configure NVARCHAR lengths and relations
            modelBuilder.Entity<Role>().Property(r => r.RoleName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Status>().Property(s => s.StatusName).HasMaxLength(100).IsRequired();

            // Additional Fluent API configurations can go here
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((AuditableEntity)entityEntry.Entity).LastChangedDate = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
