using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class SmartCoffeeDbContext : DbContext
    {
        public SmartCoffeeDbContext(DbContextOptions<SmartCoffeeDbContext> options)
            : base(options)
        {
        }

        // ===================== USERS & AUTH =====================
        public DbSet<User> Users { get; set; }
        public DbSet<PasswordResetOtp> PasswordResetOtps { get; set; }

        // ===================== SUBSCRIPTIONS =====================
        public DbSet<Plan> Plans { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }

        // ===================== PAYMENTS =====================
        public DbSet<Payment> Payments { get; set; }

        // ===================== COFFEE SYSTEM =====================
        public DbSet<CoffeeProduct> CoffeeProducts { get; set; }
        public DbSet<CoffeeUsage> CoffeeUsages { get; set; }
        public DbSet<CoffeeTransaction> CoffeeTransactions { get; set; }

        // ===================== NOTIFICATIONS =====================
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===================== USER INDEXES =====================
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.PhoneNumber)
                .IsUnique();

            // ===================== RELATIONS =====================

            modelBuilder.Entity<UserSubscription>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserSubscriptions)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(x => x.Plan)
                .WithMany(x => x.UserSubscriptions)
                .HasForeignKey(x => x.PlanID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CoffeeUsage>()
                .HasOne(x => x.User)
                .WithMany(x => x.CoffeeUsages)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasOne(x => x.User)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}