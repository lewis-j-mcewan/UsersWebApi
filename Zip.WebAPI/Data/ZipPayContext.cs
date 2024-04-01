using Microsoft.EntityFrameworkCore;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Data
{
    public partial class ZipPayContext : DbContext
    {
        public ZipPayContext()
        {
        }

        public ZipPayContext(DbContextOptions<ZipPayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.HasIndex(e => e.UserId, "accounts_userid_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.UserId)
                    .HasConstraintName("accounts_users_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "users_email_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Expenses)
                    .HasPrecision(8, 2)
                    .HasColumnName("expenses");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Salary)
                    .HasPrecision(8, 2)
                    .HasColumnName("salary");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
